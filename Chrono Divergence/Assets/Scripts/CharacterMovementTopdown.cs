using System.Collections.Generic;
using UnityEngine;

namespace ChronoDivergence
{
    public class CharacterMovementTopdown : MonoBehaviour, IMovable
    {
        [SerializeField] private float moveSpeed;
        private MouseInput mouseInput;
        [SerializeField] private Vector2 destination;
        [SerializeField] private LayerMask collisionLayers;
        private bool canMoveByInput;
        private Vector3 checkedOffset;

        public float MoveSpeed => moveSpeed;

        public bool CanMoveByInput
        {
            get => canMoveByInput;
            set => canMoveByInput = value;
        }

        private void Awake()
        {
            mouseInput = new MouseInput();
        }

        private void OnEnable()
        {
            mouseInput.Enable();
        }

        private void OnDisable()
        {
            mouseInput.Disable();
        }

        private void Start()
        {
            destination = transform.position;
        }

        private void Update()
        {
            MoveByInput();
            if (Vector2.Distance(transform.position, destination) > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            }
        }

        private void MoveByInput()
        {
            if (Vector2.Distance(transform.position, destination) < 0.1f)
            {
                checkedOffset = GetDirectionOffset(mouseInput.Keyboard.Move.ReadValue<Vector2>());
                Move();
            }
        }
        
        private bool MoveForced(Vector2 direction)
        {
            checkedOffset = direction;
            return Move();
        }

        private bool Move()
        {
            GameObject objectInFront = null;
            int originalLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (Physics2D.OverlapBox(
                    new Vector2(destination.x + checkedOffset.x * 0.9f,
                        destination.y + checkedOffset.y * 0.9f), Vector2.one * 0.9f, 0, collisionLayers))
            {
                objectInFront = Physics2D
                    .OverlapBox(
                        new Vector2(destination.x + checkedOffset.x * 0.9f,
                            destination.y + checkedOffset.y * 0.9f), Vector2.one * 0.9f, 0, collisionLayers)
                    .gameObject;
            }
            gameObject.layer = originalLayer;

            if (objectInFront)
            {
                List<IMovable> movableObject = objectInFront.GetInterfaces<IMovable>();
                if (movableObject.Count > 0)
                {
                    if (movableObject[0].MoveInDirection(checkedOffset))
                    {
                        destination = new Vector3(destination.x + checkedOffset.x,
                            destination.y + checkedOffset.y).Round(0);
                        return true;
                    }
                }
                return false;
            }
            destination = new Vector3(destination.x + checkedOffset.x,
                destination.y + checkedOffset.y);
            destination = destination.Round(0);
            return true;
        }

        private Vector2 GetDirectionOffset(Vector2 vectorInput)
        {
            if (vectorInput == Vector2.right)
            {
                return new Vector2(1, 0);
            }
            else if (vectorInput == Vector2.left)
            {
                return new Vector2(-1, 0);
            }
            else if (vectorInput == Vector2.down)
            {
                return new Vector2(0, -1);
            }
            else if (vectorInput == Vector2.up)
            {
                return new Vector2(0, 1);
            }
            else
            {
                return Vector2.zero;
            }
        }

        public int GetMovableDirections()
        {
            return 15;
        }

        public BlockTypes GetBlockType()
        {
            return BlockTypes.PLAYER;
        }

        public bool MoveInDirection(Vector2 direction)
        {
            return MoveForced(direction);
        }

        public bool CanBePushedWithOthers()
        {
            return false;
        }

        public ActivatorTypes GetActivatorType()
        {
            return ActivatorTypes.PLAYER;
        }

        public string GetBlockID()
        {
            return "";
        }

        public bool isLoadedEnough()
        {
            return true;
        }
    }
}
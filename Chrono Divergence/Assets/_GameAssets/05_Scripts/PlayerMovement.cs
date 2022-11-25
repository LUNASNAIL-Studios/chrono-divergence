using System.Collections.Generic;
using ChronoDivergence.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChronoDivergence
{
    public class PlayerMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private float moveSpeed;
        private PlayerInput playerInput;
        [SerializeField] private Vector2 destination;
        [SerializeField] private LayerMask collisionLayers;
        [SerializeField] private Animator playerAnim;
        private bool canMoveByInput;
        private Vector2 checkedOffset;
        private IActivatable targetedActivatable;
        private Vector2 lookingDirection;

        public float MoveSpeed => moveSpeed;

        public Vector2 LookingDirection => lookingDirection;

        Vector2 oldInput;

        public Vector2 Destination
        {
            get => destination;
            set => destination = value;
        }

        public IActivatable TargetedActivatable
        {
            get => targetedActivatable;
            set => targetedActivatable = value;
        }

        public bool CanMoveByInput
        {
            get => canMoveByInput;
            set => canMoveByInput = value;
        }

        private void Awake()
        {
            playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Disable();
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

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Message.Raise(new PlayerInteractEvent(lookingDirection, destination));
            }
        }

        public void OnUndoStep(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                Message.Raise(new UndoStepEvent());
            }
        }

        /// <summary>
        /// Moves the player through input from the player
        /// </summary>
        private void MoveByInput()
        {
           
            checkedOffset = playerInput.Player.Move.ReadValue<Vector2>().normalized;
            if (checkedOffset != Vector2.zero)
            {
                if (checkedOffset.x != 0)
                {
                    checkedOffset.y = 0;
                }
                lookingDirection = checkedOffset;
                playerAnim.SetFloat("horizontal", lookingDirection.x);
                playerAnim.SetFloat("vertical", lookingDirection.y);
            }

            if (Vector2.Distance(transform.position, destination) < 0.1f)
            {
                Move(false);                
            }
        }
        
        /// <summary>
        /// Moves the player without input from the player
        /// </summary>
        /// <param name="direction">(Vector2) direction to move in</param>
        /// <returns>(bool) returns if the move works successful</returns>
        private bool MoveForced(Vector2 direction, bool isUndoing = false)
        {
            checkedOffset = direction;
            lookingDirection = checkedOffset;
            if(!isUndoing)
            {
                playerAnim.SetFloat("horizontal", lookingDirection.x);
                playerAnim.SetFloat("vertical", lookingDirection.y);
            } else {
                playerAnim.SetFloat("horizontal", -lookingDirection.x);
                playerAnim.SetFloat("vertical", -lookingDirection.y);
            }
            
            if(!isUndoing)
            {
                return Move();
            } else {
                return Move(true);
            }
            
        }

        /// <summary>
        /// Moves the player
        /// </summary>
        /// <returns>(bool) returns if the move works successful</returns>
        private bool Move(bool isUndoing = false)
        {
            GameObject objectInFront = null;
            Vector2 oldDestination = destination;
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
                        Message.Raise(new PlayerMoveEvent(checkedOffset, destination, oldDestination));
                        return true;
                    }
                }
                return false;
            }
            destination = new Vector3(destination.x + checkedOffset.x,
                destination.y + checkedOffset.y);
            destination = destination.Round(0);

            if(!isUndoing)
            {
                if(oldInput != checkedOffset){
                    Message.Raise(new PlayerMoveEvent(checkedOffset, destination, destination - checkedOffset));
                }
            }
            
            return true;
        }

        /// <summary>
        /// Gets the movable direction of the IMovable
        /// </summary>
        /// <returns>int-direction in which the IMovable can move</returns>
        public int GetMovableDirections()
        {
            return 15;
        }

        public BlockTypes GetBlockType()
        {
            return BlockTypes.PLAYER;
        }

        public bool MoveInDirection(Vector2 direction, bool isUndoing = false)
        {
            if(isUndoing){
                return MoveForced(direction, true);
            } else {
                return MoveForced(direction);
            }
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

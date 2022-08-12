using TMPro;
using UnityEngine;

namespace ChronoDivergence
{
    public class MovableBlock : MonoBehaviour, IMovable
    {
        [SerializeField] private string blockID = "";
        [SerializeField] private LayerMask collisionLayers;
        [SerializeField] private int maxMoves = -1;
        [Header("Dont change:")]
        [SerializeField] private TMP_Text maxMovesText;
        [SerializeField] private TMP_Text idText;
        [SerializeField] private GameObject standardSpriteGO;
        [SerializeField] private GameObject SpriteWithIDGO;
        [SerializeField] private GameObject SpriteWithDurationGO;
        [SerializeField] private GameObject SpriteWithIDAndDurationGO;
        private Vector2 destination;
        private CharacterMovementTopdown player;
        private int movesMade = 0;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<CharacterMovementTopdown>();
            destination = gameObject.transform.position.Round(0);
            
            //Set the correct sprites and texts:
            if (blockID == "")
            {
                idText.gameObject.SetActive(false);
                SpriteWithIDGO.SetActive(false);
                SpriteWithIDAndDurationGO.SetActive(false);
            }
            else
            {
                idText.text = blockID.ToString();
                SpriteWithDurationGO.SetActive(false);
                standardSpriteGO.SetActive(false);
            }

            if (maxMoves == -1)
            {
                maxMovesText.gameObject.SetActive(false);
                SpriteWithDurationGO.SetActive(false);
                SpriteWithIDAndDurationGO.SetActive(false);
            }
            else
            {
                maxMovesText.text = maxMoves.ToString();
                SpriteWithIDGO.SetActive(false);
                standardSpriteGO.SetActive(false);
            }

            if (blockID == "" && maxMoves == -1)
            {
                idText.gameObject.SetActive(false);
                maxMovesText.gameObject.SetActive(false);
            }
        }
        
        private void Update()
        {
            if (maxMoves != -1)
            {
                maxMovesText.text = (maxMoves - movesMade).ToString();
                if (maxMoves - movesMade < 0)
                {
                    ExplodeBox();
                }
            }

            if (Vector2.Distance(transform.position, destination) > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, player.MoveSpeed * Time.deltaTime);
            }
        }

        public int GetMovableDirections()
        {
            return 15;
        }

        public BlockTypes GetBlockType()
        {
            return BlockTypes.MOVABLEBLOCK;
        }

        public bool CanBePushedWithOthers()
        {
            return false;
        }

        public ActivatorTypes GetActivatorType()
        {
            return ActivatorTypes.BLOCKS;
        }

        public string GetBlockID()
        {
            return blockID;
        }

        public bool MoveInDirection(Vector2 direction)
        {
            GameObject objectInFront = null;
            int originalLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (Physics2D.OverlapBox(
                new Vector2(destination.x + direction.x * 0.9f,
                    destination.y + direction.y * 0.9f), Vector2.one * 0.9f, 0, collisionLayers))
            {
                objectInFront = Physics2D
                    .OverlapBox(
                        new Vector2(destination.x + direction.x * 0.9f,
                            destination.y + direction.y * 0.9f), Vector2.one * 0.9f, 0, collisionLayers).gameObject;
            }
            gameObject.layer = originalLayer;
        
            Debug.Log("Checking if Object is in front...");
            if (objectInFront && objectInFront != this.gameObject)
            {
                Debug.Log("Object is in front: " + objectInFront.name);
                IMovable movableBlock = objectInFront.GetComponent<IMovable>();
                if (movableBlock != null)
                {
                    if (movableBlock.CanBePushedWithOthers())
                    {
                        if (movableBlock.MoveInDirection(direction))
                        {
                            Move(direction);
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
            }
            else
            {
                Move(direction);
                return true;
            }

            return false;
        }

        private void Move(Vector2 direction)
        {
            destination =
                new Vector2(destination.x + direction.x, destination.y + direction.y)
                    .Round(0);
            destination = destination.Round(0);
            movesMade += 1;
        }

        private void ExplodeBox()
        {
            //TODO: Make a cool animation or something maybe?
            gameObject.SetActive(false);
        }
    }
}
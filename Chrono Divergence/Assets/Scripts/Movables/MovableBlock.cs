using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChronoDivergence
{
    public class MovableBlock : MonoBehaviour, IMovable
    {
        [SerializeField] private string blockID = "";
        [SerializeField] private LayerMask collisionLayers;
        [SerializeField] private int maxMoves = -1;
        [SerializeField] private int minMoves = -1;
        [Header("Dont change:")]
        [SerializeField] private TMP_Text maxMovesText;
        [SerializeField] private TMP_Text idText;
        [SerializeField] private Image minMovesRing;
        [SerializeField] private SpriteRenderer maxMovesGo;
        [SerializeField] private SpriteRenderer minMovesGo;
        [SerializeField] private SpriteRenderer idGo;
        [SerializeField] private Color minMovesNotReached;
        [SerializeField] private Color minMovesReached;
        private Vector2 destination;
        private PlayerMovement player;
        private float movesMade = 0;

        private void OnValidate()
        {
            UpdateDisplayedParts();
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            destination = gameObject.transform.position.Round(0);
            
            UpdateDisplayedParts();
        }

        private void UpdateDisplayedParts()
        {
            int maxMovesInt = maxMoves;
            maxMovesText.text = maxMovesInt.ToString();
            idText.text = blockID.ToString();
            
            //Set the correct sprites and texts:
            if (blockID == "")
            {
                idText.color = new Color(0, 0, 0, 0);
                idGo.color = new Color(0, 0, 0, 0);
            }
            else
            {
                idText.color = new Color(0, 0, 0, 1);
                idGo.color = new Color(1, 1, 1, 1);
            }

            if (maxMoves == -1)
            {
                maxMovesText.color = new Color(0, 0, 0, 0);
                maxMovesGo.color = new Color(0, 0, 0, 0);
            }
            else
            {
                maxMovesText.color = new Color(0, 0, 0, 1);
                maxMovesGo.color = new Color(1, 1, 1, 1);
            }

            if (minMoves == -1)
            {
                minMovesGo.color = new Color(0, 0, 0, 0);
                minMovesRing.color = new Color(0, 0, 0, 0);
                minMovesRing.fillAmount = 1;
            }
            else
            {
                minMovesGo.color = new Color(1, 1, 1, 1);
                minMovesRing.color = minMovesNotReached;
                minMovesRing.fillAmount = 1f / minMoves;
            }
        }
        
        private void Update()
        {
            int maxMovesInt = maxMoves - (int)movesMade;
            maxMovesText.text = maxMovesInt.ToString();
            
            if (maxMoves != -1)
            {
                if (maxMoves - movesMade < 0)
                {
                    ExplodeBox();
                }
            }

            if (minMoves != -1)
            {
                if (movesMade < minMoves)
                {
                    minMovesRing.fillAmount = 1f / minMoves * movesMade;
                    minMovesRing.color = minMovesNotReached;
                }
                else
                {
                    minMovesRing.fillAmount = 1f;
                    minMovesRing.color = minMovesReached;
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

        public bool isLoadedEnough()
        {
            if (minMoves != -1)
            {
                return movesMade >= minMoves - 1;
            }

            return true;
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
            DOTween.To(() => movesMade, x => movesMade = x, movesMade + 1, 0.2f);
        }

        private void ExplodeBox()
        {
            //TODO: Make a cool animation or something maybe?
            gameObject.SetActive(false);
        }
    }
}
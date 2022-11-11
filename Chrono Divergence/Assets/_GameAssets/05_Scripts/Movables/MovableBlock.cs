using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChronoDivergence
{
    public class MovableBlock : MonoBehaviour, IMovable
    {
        [SerializeField] protected string blockID = "";
        [SerializeField] private LayerMask collisionLayers;
        [SerializeField] protected Color idColor;
        [SerializeField] protected Sprite idIcon;
        [SerializeField] protected SpriteRenderer idColorDisplay;
        [SerializeField] protected SpriteRenderer mainSpriteRenderer;
        protected Vector2 destination;
        protected PlayerMovement player;
        public float movesMade = 0;

        private void OnValidate()
        {
            if (idColor != null && idColorDisplay != null)
            {
                idColorDisplay.color = idColor;
            }
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            destination = gameObject.transform.position.Round(0);
            movesMade = 0;
        }
        
        protected void Update()
        {
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
            return true;
        }

        public bool MoveInDirection(Vector2 direction)
        {
            Debug.Log("Test!");
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
    }
}
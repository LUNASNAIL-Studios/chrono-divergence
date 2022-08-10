using System;
using System.ComponentModel;
using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovableBlock : MonoBehaviour, IMovable
    {
        [SerializeField] private int blockID;
        [SerializeField] private LayerMask collisionLayers;
        private Vector2 destination;
        private CharacterMovementTopdown player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<CharacterMovementTopdown>();
            destination = gameObject.transform.position.Round(0);
        }

        public int GetMovableDirections()
        {
            return 0;
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

        public int GetBlockID()
        {
            return blockID;
        }

        public bool IsMovableInDirection(Vector2 direction)
        {
            if (Vector2.Distance(transform.position, destination) < 0.1f)
            {
                GameObject objectInFront = null;
                int originalLayer = gameObject.layer;
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                if (Physics2D.OverlapBox(
                    new Vector2(transform.position.x + direction.x * 0.5f,
                        transform.position.y + direction.y * 0.5f), Vector2.one * 0.5f, 0))
                {
                    objectInFront = Physics2D
                        .OverlapBox(
                            new Vector2(transform.position.x + direction.x * 0.5f,
                                transform.position.y + direction.y * 0.5f), Vector2.one * 0.5f, 0, collisionLayers).gameObject;
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
                            if (movableBlock.IsMovableInDirection(direction))
                            {
                                destination = new Vector3(transform.position.x + direction.x,
                                    transform.position.y + direction.y, transform.position.z);
                                destination = destination.Round(0);
                                return true;
                            }

                            destination = transform.position;
                            destination = destination.Round(0);
                            return false;
                        }

                        destination = transform.position;
                        destination = destination.Round(0);
                        return false;
                    }
                }
                else
                {
                    Debug.Log("No Object in front");
                    destination =
                        new Vector2(transform.position.x + direction.x, transform.position.y + direction.y)
                            .Round(0);
                    return true;
                }
            }

            return false;
        }
        
        private void Update()
        {
            if (Vector2.Distance(transform.position, destination) > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, player.MoveSpeed * Time.deltaTime);
            }
        }
    }
}
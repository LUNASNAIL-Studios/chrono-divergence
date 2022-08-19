using System;
using UnityEngine;

namespace ChronoDivergence
{
    enum ConveyerBeltTransformations
    {
        NONE,
        INVERSE,
        ROTATE,
        TOGGLEONOFF,
    }
    public class ConveyerBelt : MonoBehaviour
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private GameObject[] buttonsThatTriggerChange;
        [SerializeField] private ConveyerBeltTransformations transformationType;
        [SerializeField] private bool initiallyMoving;
        [SerializeField] private GameObject spriteHolder;
        private bool isMoving = true;

        private void OnValidate()
        {
            UpdateDirection();
        }

        private void Start()
        {
            if (transformationType == ConveyerBeltTransformations.TOGGLEONOFF)
            {
                if (initiallyMoving == true)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }
            }
            else
            {
                isMoving = true;
            }
        }

        private void UpdateDirection()
        {
            if (direction == Vector2.right)
            {
                spriteHolder.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            } else if (direction == Vector2.down)
            {
                spriteHolder.transform.eulerAngles = new Vector3(0f, 0f, 270f);
            } else if (direction == Vector2.left)
            {
                spriteHolder.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            } else if (direction == Vector2.up)
            {
                spriteHolder.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetInterfaces<IMovable>().Count > 0)
            {
                IMovable movable = col.gameObject.GetInterfaces<IMovable>()[0];
                if (isMoving)
                {
                    Debug.Log("Pushing!" + movable.MoveInDirection(direction));
                }
            }
        }
    }
}
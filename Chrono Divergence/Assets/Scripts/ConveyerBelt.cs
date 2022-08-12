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

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetInterfaces<IMovable>().Count > 0)
            {
                CharacterMovementTopdown character = null;
                if (col.CompareTag("Player"))
                {
                    character = col.gameObject.GetComponent<CharacterMovementTopdown>();
                }
                IMovable movable = col.gameObject.GetInterfaces<IMovable>()[0];
                if (isMoving)
                {
                    Debug.Log("Pushing!" + movable.MoveInDirection(direction));
                }
            }
        }
    }
}
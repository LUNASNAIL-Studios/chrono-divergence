using System;
using DG.Tweening;
using UnityEngine;

namespace ChronoDivergence
{
    enum ConveyerBeltTransformations
    {
        NONE,
        INVERSE,
        ROTATE,
        ROTATEFURTHER,
        TOGGLEONOFF,
    }
    public class ConveyerBelt : MonoBehaviour
    {
        
        [SerializeField] private GameObject[] buttonsThatTriggerChange;
        [SerializeField] private ConveyerBeltTransformations transformationType;
        [SerializeField] private bool initiallyMoving;
        [Header("Nicht verändern:")]
        [SerializeField] private GameObject spriteHolder;
        [SerializeField] private Vector2 direction;
        private Vector2 initialDirection;
        private bool isMoving = true;
        private bool activated;

        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        private void OnValidate()
        {
            UpdateDirection();
        }

        private void Start()
        {
            initialDirection = direction;
            
            if (transformationType == ConveyerBeltTransformations.TOGGLEONOFF)
            {
                if (initiallyMoving)
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

        private void Update()
        {
            CheckActivators();
        }
        
        private void CheckActivators()
        {
            bool allActive = true;
            foreach (GameObject activatable in buttonsThatTriggerChange)
            {
                IActivatable activatableThing = activatable.GetInterfaces<IActivatable>()[0];
                if (!activatableThing.IsActivated())
                {
                    allActive = false;
                }
            }

            if (allActive)
            {
                if (!activated)
                {
                    switch (transformationType)
                    {
                        case ConveyerBeltTransformations.NONE:
                            break;
                        case ConveyerBeltTransformations.ROTATE:
                            transform.DORotate(transform.eulerAngles + Quaternion.AngleAxis(90, Vector3.back).eulerAngles,
                                0.2f);
                            direction = direction.Rotate(-90f);
                            direction.Normalize();
                            break;
                        case ConveyerBeltTransformations.ROTATEFURTHER:
                            transform.DORotate(transform.eulerAngles + Quaternion.AngleAxis(-90, Vector3.back).eulerAngles,
                                0.2f);
                            direction = direction.Rotate(90f);
                            direction.Normalize();
                            break;
                        case ConveyerBeltTransformations.INVERSE:
                            transform.DORotate(
                                transform.eulerAngles + Quaternion.AngleAxis(180, Vector3.back).eulerAngles, 0.2f);
                            direction = -initialDirection;
                            break;
                        case ConveyerBeltTransformations.TOGGLEONOFF:
                            isMoving = !initiallyMoving;
                            break;
                    }

                    activated = true;
                }
            }
            else
            {
                if (activated)
                {
                    switch (transformationType)
                    {
                        case ConveyerBeltTransformations.NONE:
                            break;
                        case ConveyerBeltTransformations.ROTATE:
                            transform.DORotate(
                                transform.eulerAngles + Quaternion.AngleAxis(-90, Vector3.back).eulerAngles, 0.2f);
                            direction = direction.Rotate(90f);
                            direction.Normalize();
                            break;
                        case ConveyerBeltTransformations.ROTATEFURTHER:
                            transform.DORotate(
                                transform.eulerAngles + Quaternion.AngleAxis(-90, Vector3.back).eulerAngles, 0.2f);
                            direction = direction.Rotate(90f);
                            direction.Normalize();
                            break;
                        case ConveyerBeltTransformations.INVERSE:
                            transform.DORotate(
                                transform.eulerAngles + Quaternion.AngleAxis(-180, Vector3.back).eulerAngles, 0.2f);
                            direction = initialDirection;
                            break;
                        case ConveyerBeltTransformations.TOGGLEONOFF:
                            isMoving = initiallyMoving;
                            break;
                    }

                    activated = false;
                }
            }
        }

        public void UpdateDirection()
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
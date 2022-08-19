using System;
using ChronoDivergence.Events;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ChronoDivergence
{
    public class Lever : MonoBehaviour, IActivatable
    {
        [SerializeField] private UnityEvent OnActivation;
        [SerializeField] private UnityEvent OnDeactivation;
        [SerializeField] private bool isActivated;
        [SerializeField] private bool isActivatable;
        [Header("Nicht verändern:")]
        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Vector2 direction;
        private PlayerMovement player;

        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void OnValidate()
        {
            UpdateDirection();
        }

        private void OnEnable()
        {
            Message<PlayerInteractEvent>.Add(OnInteractEvent);
            Message<PlayerMoveEvent>.Add(OnPlayerMoveEvent);
        }
        
        private void OnDisable()
        {
            Message<PlayerInteractEvent>.Remove(OnInteractEvent);
            Message<PlayerMoveEvent>.Remove(OnPlayerMoveEvent);
        }

        private void OnInteractEvent(PlayerInteractEvent ctx)
        {
            if (isActivatable)
            {
                if ((Lever) player.TargetedActivatable == this)
                {
                    ToggleButton();
                }
            }
        }
        
        public void UpdateDirection()
        {
            if (direction == Vector2.right)
            {
                spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            } else if (direction == Vector2.down)
            {
                spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, 270f);
            } else if (direction == Vector2.left)
            {
                spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            } else if (direction == Vector2.up)
            {
                spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }
        }
        
        private void ToggleButton()
        {
            isActivated = !isActivated;
            if (isActivated)
            {
                OnActivation.Invoke();
            }
            else
            {
                OnDeactivation.Invoke();
            }
            anim.SetBool("activated", isActivated);
        }

        private void OnPlayerMoveEvent(PlayerMoveEvent ctx)
        {
            if (player.Destination == (Vector2)gameObject.transform.position)
            {
                if (player.LookingDirection == direction)
                {
                    player.TargetedActivatable = this;
                    anim.SetBool("targeted", true);
                }
                else
                {
                    if ((Lever)player.TargetedActivatable == this)
                    {
                        player.TargetedActivatable = null;
                        anim.SetBool("targeted", false);
                    }
                }
            }
            else
            {
                if ((Lever)player.TargetedActivatable == this)
                {
                    player.TargetedActivatable = null;
                    anim.SetBool("targeted", false);
                }
            }
        }

        public bool IsActivated()
        {
            return isActivated;
        }

        public bool IsActivatable()
        {
            return isActivatable;
        }

        public void SetActivatable(bool activatable)
        {
            isActivatable = activatable;
        }

        public string GetRequiredBlockID()
        {
            return ""; //No BlockID needed for lever
        }

        public ActivatorTypes[] GetActivatingTypes()
        {
            return new ActivatorTypes[]{ActivatorTypes.ALL};
        }
    }
}
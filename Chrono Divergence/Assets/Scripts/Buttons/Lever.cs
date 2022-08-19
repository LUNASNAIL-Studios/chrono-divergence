using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ChronoDivergence
{
    public class Lever : MonoBehaviour, IActivatable
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private UnityEvent OnActivation;
        [SerializeField] private UnityEvent OnDeactivation;
        [SerializeField] private bool isActivated;
        [SerializeField] private bool isActivatable;
        [Header("Nicht verändern!")]
        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private PlayerMovement player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void OnValidate()
        {
            UpdateDirection();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (player.LookingDirection == direction)
                {
                    player.TargetedActivatable = this;
                    anim.SetBool("targeted", true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if ((Lever)player.TargetedActivatable == this)
                {
                    player.TargetedActivatable = null;
                    anim.SetBool("targeted", false);
                }
            }
        }

        private void InteractEvent()
        {
            if ((Lever)player.TargetedActivatable == this)
            {
                ToggleButton();
            }
        }
        
        private void UpdateDirection()
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
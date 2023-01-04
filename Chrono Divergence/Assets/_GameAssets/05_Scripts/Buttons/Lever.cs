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
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite spriteON;
        [SerializeField] private Sprite spriteOFF;
        [SerializeField] private Vector2 direction;
        [SerializeField] [Range(0,5)] private float cooldownDuration;
        private PlayerMovement player;
        private float leverCooldown;

        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            direction = Vector2.left;
            UpdateDirection();
        }

        private void Update()
        {
            if (leverCooldown > 0)
            {
                leverCooldown -= Time.deltaTime;
            }
            else
            {
                leverCooldown = 0;
            }
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
            if (leverCooldown <= 0)
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
                if(isActivated){
                    spriteRenderer.sprite = spriteON;
                } else {
                    spriteRenderer.sprite = spriteOFF;
                }
                leverCooldown = cooldownDuration;
            }
        }

        private void OnPlayerMoveEvent(PlayerMoveEvent ctx)
        {
            if (player.Destination == (Vector2)gameObject.transform.position)
            {
                player.TargetedActivatable = this;
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                if ((Lever)player.TargetedActivatable == this)
                {
                    player.TargetedActivatable = null;
                    spriteRenderer.color = Color.white;
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
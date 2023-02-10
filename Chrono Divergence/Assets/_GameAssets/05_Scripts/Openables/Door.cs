using UnityEngine;

namespace ChronoDivergence
{
    public class Door : MonoBehaviour, IOpenable
    {
        [SerializeField] private Animator anim;
        [SerializeField] private bool isOpen;
        [SerializeField] private bool isInverted;
        [SerializeField] private bool isActivatable;
        [SerializeField] private GameObject[] neededIActivators;
        [SerializeField] private Sprite doorOpenSprite;
        [SerializeField] private Sprite doorClosedSprite;
        [SerializeField] private SpriteRenderer doorSpriteRenderer;
        [SerializeField] private Collider2D collider;
            
        public bool IsOpened()
        {
            return isOpen;
        }

        public bool IsActivatable()
        {
            return isActivatable;
        }

        public void SetActivatable(bool activatable)
        {
            isActivatable = activatable;
        }

        private void Update()
        {
            //TODO: Implement with events!
            CheckActivators();
        }

        private void CheckActivators()
        {
            bool allActive = true;
            foreach (GameObject activatable in neededIActivators)
            {
                IActivatable activatableThing = activatable.GetInterfaces<IActivatable>()[0];
                if (!activatableThing.IsActivated())
                {
                    allActive = false;
                }
            }

            if (allActive)
            {
                isOpen = !isInverted;
                //anim.SetBool("IsOpen", !isInverted);
                if(collider)
                {
                    collider.enabled = isInverted;
                }
                doorSpriteRenderer.sprite = doorOpenSprite;
            }
            else
            {
                isOpen = isInverted;
                //anim.SetBool("IsOpen", isInverted);
                if(collider)
                {
                    collider.enabled = !isInverted;
                }
                doorSpriteRenderer.sprite = doorClosedSprite;
            }
        }
    }
}
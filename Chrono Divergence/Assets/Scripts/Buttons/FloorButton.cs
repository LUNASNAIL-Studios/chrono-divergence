using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ChronoDivergence
{
    public class FloorButton : MonoBehaviour, IActivatable
    {

        [SerializeField] private bool isActivated;
        [SerializeField] private bool isActivatable;
        [SerializeField] private string requiredBlockID = "";
        [SerializeField] private ActivatorTypes[] activatingThings;
        [SerializeField] private UnityEvent OnActivation;
        [SerializeField] private UnityEvent OnDeactivation;
        [SerializeField] private TMP_Text idText;

        private void Start()
        {
            idText.text = requiredBlockID;
        }

        private void OnValidate()
        {
            idText.text = requiredBlockID;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInterfaces<IMovable>().Count > 0)
            {
                IMovable movableThing = other.gameObject.GetInterfaces<IMovable>()[0];
                foreach (ActivatorTypes activatingType in activatingThings)
                {
                    if (movableThing.GetActivatorType() == activatingType)
                    {
                        if (movableThing.isLoadedEnough())
                        {
                            if (requiredBlockID == "" || movableThing.GetBlockID()
                                    .Equals(requiredBlockID, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ActivateButton();
                            }
                        }
                    }
                }
            } else if (other.gameObject.CompareTag("Player"))
            {
                foreach (ActivatorTypes activatingType in activatingThings)
                {
                    if (activatingType == ActivatorTypes.PLAYER)
                    {
                        ActivateButton();
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (isActivated)
            {
                DeActivateButton();
            }
        }
        
        private void ActivateButton()
        {
            isActivated = true;
            OnActivation.Invoke();
        }
        
        private void DeActivateButton()
        {
            isActivated = false;
            OnDeactivation.Invoke();
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
            return requiredBlockID;
        }

        public ActivatorTypes[] GetActivatingTypes()
        {
            return activatingThings;
        }
    }
}
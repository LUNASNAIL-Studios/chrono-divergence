using UnityEngine;
using UnityEngine.Events;

namespace ChronoDivergence
{
    public class Firework : MonoBehaviour, IOpenable
    {
        [SerializeField] private UnityEvent OnStart;
        [SerializeField] private GameObject[] neededIActivators;
            
        public bool IsOpened()
        {
            return false;
        }

        public bool IsActivatable()
        {
            return true;
        }

        public void SetActivatable(bool activatable)
        {
            //Do nothing
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
                OnStart.Invoke();
            }
            else
            {
            }
        }
    }
}
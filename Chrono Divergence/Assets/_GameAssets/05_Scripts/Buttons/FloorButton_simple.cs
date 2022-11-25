using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ChronoDivergence
{
    public class FloorButton_simple : FloorButton
    {

        private void Start()
        {
        }

        private void OnValidate()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetInterfaces<IMovable>().Count > 0)
            {
                IMovable movableThing = other.gameObject.GetInterfaces<IMovable>()[0];
                foreach (ActivatorTypes activatingType in activatingThings)
                {
                    Debug.Log("Test1");
                    if (movableThing.GetActivatorType() == activatingType || activatingType == ActivatorTypes.ALL)
                    {
                        Debug.Log("Test2");
                        if (movableThing.isLoadedEnough())
                        {
                                Debug.Log("Test3");
                                ActivateButton();
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
    }
}

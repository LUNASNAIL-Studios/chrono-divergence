using System.Collections;
using System.Collections.Generic;
using ChronoDivergence;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour, IActivatable, ILaserInteractor
{
    [SerializeField] private UnityEvent OnActivation;
    [SerializeField] private UnityEvent OnDeactivation;
    [SerializeField] private bool isActivated;
    [SerializeField] private bool isActivatable;

    public void ReceiveLaser()
    {
        if (isActivatable)
        {
            isActivated = true;
            OnActivation.Invoke();
        }
    }

    public void LoseLaser()
    {
        Debug.Log("Should lose laser");
        if (isActivatable)
        {
            isActivated = false;
            OnDeactivation.Invoke();
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
        return "";
    }

    public ActivatorTypes[] GetActivatingTypes()
    {
        return new ActivatorTypes[]{ActivatorTypes.ALL};
    }
}

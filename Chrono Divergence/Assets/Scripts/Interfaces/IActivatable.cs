using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IActivatable
    {
        bool IsActivated();
        bool IsActivatable();
        void SetActivatable(bool activatable);
        string GetRequiredBlockID();

        ActivatorTypes[] GetActivatingTypes();
    }
}
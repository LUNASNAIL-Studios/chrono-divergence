using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IMovable
    {
        int GetMovableDirections();
        BlockTypes GetBlockType();
        bool IsMovableInDirection(Vector2 direction);
        bool CanBePushedWithOthers();
        ActivatorTypes GetActivatorType();
        int GetBlockID();
    }
}
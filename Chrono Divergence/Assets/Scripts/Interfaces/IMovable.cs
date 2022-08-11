using DefaultNamespace.Enums;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IMovable
    {
        int GetMovableDirections();
        BlockTypes GetBlockType();
        bool MoveInDirection(Vector2 direction);
        bool CanBePushedWithOthers();
        ActivatorTypes GetActivatorType();
        string GetBlockID();
    }
}
using UnityEngine;

namespace ChronoDivergence
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
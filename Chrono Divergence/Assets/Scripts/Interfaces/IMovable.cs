using UnityEngine;

namespace ChronoDivergence
{
    public interface IMovable
    {
        /// <summary>
        /// Gets the movable direction of the IMovable
        /// </summary>
        /// <returns>(int) int-direction in which the IMovable can move</returns>
        int GetMovableDirections();
        
        /// <summary>
        /// Gets the blocktype of the IMovable
        /// </summary>
        /// <returns>(BlockTypes) Type of Block</returns>
        BlockTypes GetBlockType();
        
        /// <summary>
        /// Moves the IMovable in the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>(bool) returns if the move works successfully</returns>
        bool MoveInDirection(Vector2 direction);
        
        /// <summary>
        /// Returns if the IMovable can be pushed with others
        /// </summary>
        /// <returns>(bool) returns it can be moved with others</returns>
        bool CanBePushedWithOthers();
        
        /// <summary>
        /// Returns the ActivatorType-Category
        /// </summary>
        /// <returns>(ActivatorTypes) Category of Activator</returns>
        ActivatorTypes GetActivatorType();
        
        /// <summary>
        /// Returns the set BlockID
        /// </summary>
        /// <returns>(string) BlockID of IMovable</returns>
        string GetBlockID();
        
        /// <summary>
        /// Returns if the IMovable is loaded enough to activate something
        /// </summary>
        /// <returns>(bool) Returns if the loading is enough</returns>
        bool isLoadedEnough();
    }
}
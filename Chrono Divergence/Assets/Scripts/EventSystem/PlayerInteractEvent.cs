using UnityEngine;

namespace ChronoDivergence.Events
{
    public class PlayerInteractEvent
    {
        private Vector2 lookingDirection;
        private Vector2 position;

        public Vector2 LookingDirection => lookingDirection;

        public Vector2 Position => position;

        public PlayerInteractEvent(Vector2 lookingDirection, Vector2 position)
        {
            this.lookingDirection = lookingDirection;
            this.position = position;
        }
    }
}
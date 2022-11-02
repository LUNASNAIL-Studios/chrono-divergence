using UnityEngine;

namespace ChronoDivergence.Events
{
    public class PlayerMoveEvent
    {
        private Vector2 direction;
        private Vector2 newPosition;
        private Vector2 oldPosition;

        public Vector2 Direction => direction;
        public Vector2 NewPosition => newPosition;
        public Vector2 OldPosition => oldPosition;

        public PlayerMoveEvent(Vector2 direction, Vector2 newPosition, Vector2 oldPosition)
        {
            this.direction = direction;
            this.newPosition = newPosition;
            this.oldPosition = oldPosition;
        }
    }
}
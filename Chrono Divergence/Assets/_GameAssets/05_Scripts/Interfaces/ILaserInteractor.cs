using UnityEngine;

namespace ChronoDivergence
{
    public interface ILaserInteractor
    {
        public void ReceiveLaser(Vector2 direction);
        public void LoseLaser();
    }
}
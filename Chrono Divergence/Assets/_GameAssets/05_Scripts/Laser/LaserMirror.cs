using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ChronoDivergence
{
    public class LaserMirror : MonoBehaviour, ILaserInteractor
    {
        [SerializeField] private LineRenderer laserLineRenderer;
        [SerializeField] private Light2D laserLight;
        [SerializeField] private Transform laserOutput;
        private ILaserInteractor activeLaserInteractor;
        [SerializeField] private bool hasLaserInput;
        [SerializeField] private GameObject reflectiveSurface;
        private Vector2 reflectedVector;

        void FixedUpdate()
        {
            if (hasLaserInput)
            {
                laserLineRenderer.SetPosition(0, laserOutput.position);
                RaycastHit2D hit = Physics2D.Raycast(laserOutput.position, reflectedVector, 100);
                if (hit.collider)
                {
                    var position = laserOutput.localPosition;
                    var endPosition = (Vector2)laserOutput.position - hit.point;
                    laserLineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, position.z));
                    laserLight.shapePath[0] = position;
                    laserLight.shapePath[1] = position;
                    laserLight.shapePath[2] = endPosition;
                    laserLight.shapePath[3] = endPosition;

                    if (hit.collider.gameObject.GetInterfaces<ILaserInteractor>().Count > 0)
                    {
                        ILaserInteractor laserInteractor = hit.collider.gameObject.GetInterfaces<ILaserInteractor>()[0];
                        if (activeLaserInteractor != laserInteractor)
                        {
                            if (activeLaserInteractor != null)
                            {
                                activeLaserInteractor.LoseLaser();
                            }

                            activeLaserInteractor = null;
                            activeLaserInteractor = laserInteractor;
                        }

                        laserInteractor.ReceiveLaser(transform.up);
                    }
                    else
                    {
                        if (activeLaserInteractor != null)
                        {
                            activeLaserInteractor.LoseLaser();
                        }

                        activeLaserInteractor = null;
                    }
                }
                else
                {
                    laserLineRenderer.SetPosition(1, transform.up * 2000);
                }
            }
            else
            {
                if (activeLaserInteractor != null)
                {
                    activeLaserInteractor.LoseLaser();
                    activeLaserInteractor = null;
                }
            }
        }

        public void ReceiveLaser(Vector2 direction)
        {
            reflectedVector = Vector2.Reflect(-direction, reflectiveSurface.transform.up);
            float angle = Vector2.SignedAngle(-direction, reflectiveSurface.transform.up);
            
            Debug.Log(angle);
            if (angle > 0)
            {
                hasLaserInput = true;
                laserLineRenderer.enabled = true;
            }
            else
            {
                hasLaserInput = false;
                laserLineRenderer.enabled = false;
            }
        }

        public void LoseLaser()
        {
            hasLaserInput = false;
        }
    }
}
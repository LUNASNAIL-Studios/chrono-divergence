using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ChronoDivergence
{
    public class LaserOutput : MonoBehaviour
    {
        [SerializeField] private LineRenderer laserLineRenderer;
        [SerializeField] private Light2D laserLight;
        [SerializeField] private Transform laserOutput;
        private ILaserInteractor activeLaserInteractor;

        void FixedUpdate()
        {
            laserLineRenderer.SetPosition(0, transform.position - transform.up * 0.5f);
            RaycastHit2D hit = Physics2D.Raycast(laserOutput.position, transform.up, 100);
            if (hit.collider)
            {
                var position = laserOutput.localPosition;
                laserLineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, position.z));
                laserLight.shapePath[0] = position + Vector3.right * 0.1f - Vector3.up * 0.5f;
                laserLight.shapePath[1] = position + Vector3.right * -0.1f - Vector3.up * 0.5f;
                laserLight.shapePath[2] = (Vector3.up * Vector3.Distance(laserOutput.position, hit.point)) + Vector3.right * 0.1f;
                laserLight.shapePath[3] = (Vector3.up * Vector3.Distance(laserOutput.position, hit.point)) + Vector3.right * -0.1f;
                
                if (hit.collider.gameObject.GetInterfaces<ILaserInteractor>().Count > 0)
                {
                    ILaserInteractor laserInteractor = hit.collider.gameObject.GetInterfaces<ILaserInteractor>()[0];
                    if (activeLaserInteractor != laserInteractor)
                    {
                        if (activeLaserInteractor != null) {
                            activeLaserInteractor.LoseLaser();
                        }   
                        activeLaserInteractor = null;
                        activeLaserInteractor = laserInteractor;
                    }
                    laserInteractor.ReceiveLaser(transform.up);
                }
                else
                {
                    if (activeLaserInteractor != null) {
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
    }
}
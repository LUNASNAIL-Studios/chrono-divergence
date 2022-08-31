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

        void FixedUpdate()
        {
            laserLineRenderer.SetPosition(0, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(laserOutput.position, transform.up);
            if (hit.collider)
            {
                var position = laserOutput.localPosition;
                laserLineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, position.z));
                laserLight.shapePath[0] = position + Vector3.right * 0.1f;
                laserLight.shapePath[1] = position + Vector3.right * -0.1f;
                laserLight.shapePath[2] = Vector3.up * Vector3.Distance(laserOutput.position, hit.point) + Vector3.right * 0.1f;
                laserLight.shapePath[3] = Vector3.up * Vector3.Distance(laserOutput.position, hit.point) + Vector3.right * -0.1f;
            }
            else
            {
                laserLineRenderer.SetPosition(1, transform.up * 2000);
                Debug.Log("Did not Hit");
            }
        }
    }
}
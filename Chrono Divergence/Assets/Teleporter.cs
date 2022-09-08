using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ChronoDivergence
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Color portalColor;
        [SerializeField] private Teleporter targetTeleporter;
        [SerializeField] private SpriteRenderer portalRenderer;
        [SerializeField] private Light2D portalLight;
        [SerializeField] private bool isVisible;
        private bool justUsed;
        private PlayerMovement player;

        public Teleporter TargetTeleporter
        {
            get => targetTeleporter;
            set => targetTeleporter = value;
        }

        public Color PortalColor
        {
            get => portalColor;
            set => portalColor = value;
        }

        public Light2D PortalLight
        {
            get => portalLight;
            set => portalLight = value;
        }

        public bool JustUsed
        {
            get => justUsed;
            set => justUsed = value;
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        private void OnValidate()
        {
            UpdatePortal();
            if (targetTeleporter)
            {
                targetTeleporter.TargetTeleporter = this;
                targetTeleporter.PortalColor = portalColor;
                targetTeleporter.UpdatePortal();
            }
        }

        public void UpdatePortal()
        {
            if (isVisible)
            {
                portalLight.color = portalColor;
                portalRenderer.color = new Color(portalColor.r, portalColor.g, portalColor.b, 1);
            }
            else
            {
                portalLight.color = Color.black;
                portalRenderer.color = new Color(portalColor.r, portalColor.g, portalColor.b, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (targetTeleporter)
            {
                if (!justUsed && isVisible)
                {
                    if (col.CompareTag("Player"))
                    {
                        targetTeleporter.JustUsed = true;
                        Vector3 targetPosition = targetTeleporter.gameObject.transform.position;
                        player.Destination = targetPosition;
                        player.transform.position = targetPosition;
                        justUsed = false;
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            justUsed = false;
        }
    }

        
}
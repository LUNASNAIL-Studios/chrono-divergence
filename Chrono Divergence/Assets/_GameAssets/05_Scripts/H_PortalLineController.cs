using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class H_PortalLineController : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private SpriteRenderer lineRender;
    [SerializeField] private Sprite lineInactive;
    [SerializeField] private Sprite lineActive;
    [SerializeField] private H_PortalController connectedPortal;
    [SerializeField] private Light2D portalLineLight;

    private void OnValidate()
    {
        if (!connectedPortal) return;
        lineRender.sprite = isActive ? lineActive : lineInactive;
        lineRender.color = connectedPortal.PortalColor;
        portalLineLight.gameObject.SetActive(isActive);
        portalLineLight.color = connectedPortal.PortalColor;
    }

    private void Update()
    {
        
        if (!connectedPortal) return;
        
        isActive = connectedPortal.IsAccessible;
        lineRender.sprite = connectedPortal.IsAccessible ? lineActive : lineInactive;
        lineRender.color = connectedPortal.IsAccessible ? connectedPortal.PortalColor : Color.gray;
        portalLineLight.gameObject.SetActive(isActive);
        portalLineLight.color = connectedPortal.PortalColor;
    }
}

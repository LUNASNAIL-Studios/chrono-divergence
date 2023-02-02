using System;
using System.Collections;
using System.Collections.Generic;
using ChronoDivergence;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class H_PortalController : MonoBehaviour, IActivatable
{
    [Header("Change here")]
    [SerializeField] private int connectedLevel;
    [SerializeField] private Color colorLevelOff;
    [SerializeField] private Color colorLevelAccessible;
    [SerializeField] private Color colorLevelCompleted;
    [TextArea(3,10)][SerializeField] private string levelTitle;
    
    [Header("Don't change this:")]
    [SerializeField] private bool isAccessible;
    [SerializeField] private bool isCompleted;
    [SerializeField] private GameObject portalLight;
    [SerializeField] private SpriteRenderer portalLightRenderer;
    [SerializeField] private Light2D portalLightLight;
    [SerializeField] private GameObject portalUI;
    [SerializeField] private GameObject portalInactiveUI;
    [SerializeField] private TMP_Text portalUITitleText;
    [SerializeField] private LevelButton levelButton;

    public Color ColorLevelOff => colorLevelOff;
    public Color ColorLevelCompleted => colorLevelCompleted;
    public Color ColorLevelAccessible => colorLevelAccessible;
    public bool IsAccessible => isAccessible;
    public bool IsCompleted => isCompleted;

    private void OnValidate()
    {
        portalUITitleText.text = levelTitle;
        
        portalLight.SetActive(isAccessible);
        if (isCompleted) {
            portalLightRenderer.color = colorLevelCompleted;
            portalLightLight.color = colorLevelCompleted;
        } else if (isAccessible) {
            portalLightRenderer.color = colorLevelAccessible;
            portalLightLight.color = colorLevelAccessible;
        } else {
            portalLightRenderer.color = colorLevelOff;
            portalLightLight.color = colorLevelOff;
        }
        
        portalUI.SetActive(isAccessible);
        portalInactiveUI.SetActive(!isAccessible);
        levelButton.SetLevel = "Level " + connectedLevel;
    }

    private void Start()
    {
        portalUI.SetActive(false);
        portalInactiveUI.SetActive(false);
        
        if (connectedLevel > 1) {
            isAccessible = PlayerPrefs.GetInt("LEVELCOMPLETED_" + (connectedLevel - 1)) == 1;
        } else {
            isAccessible = true;
        }

        isCompleted = PlayerPrefs.GetInt("LEVELCOMPLETED_" + connectedLevel) == 1;
        portalLight.SetActive(isAccessible);
        
        if (isCompleted) {
            portalLightRenderer.color = colorLevelCompleted;
            portalLightLight.color = colorLevelCompleted;
        } else if (isAccessible) {
            portalLightRenderer.color = colorLevelAccessible;
            portalLightLight.color = colorLevelAccessible;
        } else {
            portalLightRenderer.color = colorLevelOff;
            portalLightLight.color = colorLevelOff;
        }

        portalUITitleText.text = levelTitle;
        levelButton.SetLevel = "Level " + connectedLevel;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isAccessible)
            {
                portalUI.SetActive(true);
            }
            else
            {
                portalInactiveUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        portalUI.SetActive(false);
        portalInactiveUI.SetActive(false);
    }

    public bool IsActivated()
    {
        return isCompleted;
    }

    public bool IsActivatable()
    {
        return true;
    }

    public void SetActivatable(bool activatable)
    { }

    public string GetRequiredBlockID()
    {
        return "";
    }

    public ActivatorTypes[] GetActivatingTypes()
    {
        return null;
    }
}

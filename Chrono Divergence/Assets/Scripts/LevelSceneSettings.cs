using System.Collections.Generic;
using ChronoDivergence;
using UnityEngine;

public class LevelSceneSettings : MonoBehaviour
{
    [SerializeField] private float lightintensity;
    [SerializeField] private List<LevelSceneSettingsGetter> getters;

    public float Lightintensity
    {
        get => lightintensity;
    }

    private void OnValidate()
    {
        getters.Clear();
        LevelSceneSettingsGetter[] gos = GameObject.FindObjectsOfType<LevelSceneSettingsGetter>();
        foreach (LevelSceneSettingsGetter levelSceneSettingsGetter in gos)
        {
            getters.Add(levelSceneSettingsGetter);
        }

        foreach (LevelSceneSettingsGetter levelSceneSettingsGetter in getters)
        {
            levelSceneSettingsGetter.UpdateValues();
        }
    }
}

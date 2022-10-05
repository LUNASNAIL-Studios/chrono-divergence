using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ChronoDivergence
{
    public class LevelSceneSettingsGetter : MonoBehaviour
    {
        [SerializeField] private Light2D globalLightSource;
        private LevelSceneSettings levelSettings;

        private void Start()
        {
            if (GameObject.FindWithTag("LevelSceneSettings"))
            {
                levelSettings = GameObject.FindWithTag("LevelSceneSettings").GetComponent<LevelSceneSettings>();
            }

            UpdateValues();
        }

        private void OnValidate()
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (levelSettings != null && globalLightSource != null)
            {
                globalLightSource.intensity = levelSettings.Lightintensity;
            }
        }
    }
}
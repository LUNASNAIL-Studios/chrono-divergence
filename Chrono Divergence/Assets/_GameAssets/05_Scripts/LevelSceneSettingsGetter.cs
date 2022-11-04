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
            if (GameObject.FindWithTag("SceneSettings"))
            {
                levelSettings = GameObject.FindWithTag("SceneSettings").GetComponent<LevelSceneSettings>();
            }

            UpdateValues();
        }

        private void OnValidate()
        {
            UpdateValues();
        }

        public void UpdateValues()
        {
            if (levelSettings == null && GameObject.FindWithTag("SceneSettings"))
            {
                levelSettings = GameObject.FindWithTag("SceneSettings").GetComponent<LevelSceneSettings>();
            }
            
            if (levelSettings != null && globalLightSource != null)
            {
                globalLightSource.intensity = levelSettings.Lightintensity;
            }
        }
    }
}
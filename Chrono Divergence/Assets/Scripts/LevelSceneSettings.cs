using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneSettings : MonoBehaviour
{
    [SerializeField] private float lightintensity;

    public float Lightintensity
    {
        get => lightintensity;
    }
}

using System;
using Cinemachine;
using UnityEngine;

namespace ChronoDivergence
{
    public class LevelFinish : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Level Finished!");
                //TODO: Alot of stuff!
            }
        }
    }
}
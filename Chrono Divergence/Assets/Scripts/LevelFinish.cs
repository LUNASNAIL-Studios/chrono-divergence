using System;
using Cinemachine;
using UnityEngine;

namespace ChronoDivergence
{
    public class LevelFinish : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Start()
        {
            canvas.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Level Finished!");
                canvas.SetActive(true);
                //TODO: Alot of stuff!
            }
        }
    }
}
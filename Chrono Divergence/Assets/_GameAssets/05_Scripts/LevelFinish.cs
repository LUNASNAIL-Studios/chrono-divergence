using System;
using Cinemachine;
using UnityEngine;

namespace ChronoDivergence
{
    public class LevelFinish : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private int levelID;

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
                PlayerPrefs.SetInt("LEVELCOMPLETED_" + levelID, 1);
                //TODO: Alot of stuff!
            }
        }
    }
}
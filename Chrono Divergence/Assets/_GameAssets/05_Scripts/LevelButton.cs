using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChronoDivergence
{
    public class LevelButton : MonoBehaviour
    {
        public void OpenLevel(string sceneName)
        {
            //TODO: Check with LevelManager if level is accessible
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ZoomLevel()
        {
            GameObject.FindWithTag("Player");
        }
    }
}
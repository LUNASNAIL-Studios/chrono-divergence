using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChronoDivergence
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private string setLevel;

        public string SetLevel
        {
            get => setLevel;
            set => setLevel = value;
        }

        public void OpenLevel(string sceneName)
        {
            //TODO: Check with LevelManager if level is accessible
            SceneManager.LoadScene(sceneName);
        }
        
        public void OpenSetLevel()
        {
            //TODO: Check with LevelManager if level is accessible
            SceneManager.LoadScene(setLevel);
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
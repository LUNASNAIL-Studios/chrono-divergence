using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChronoDivergence
{
    //Checks wether a required Level is completed and activates this
    public class LevelCompletedChecker : MonoBehaviour
    {
        [SerializeField] private bool isCompletedByDefault;
        [SerializeField] private bool isCompletedByOtherChecker;
        [SerializeField] private List<LevelCompletedChecker> otherChecker;
        [SerializeField] private bool isCompletedByLevel;
        [SerializeField] private int requiredLevelID;
        [SerializeField] private Color ActivatedColor;
        [SerializeField] private Color DeactivatedColor;
        [SerializeField] private List<Image> changedImages;
        [SerializeField] private Button changedButton;
        private bool completedChecked;

        public bool CompletedChecked => completedChecked;

        private void Update()
        {
            if (isCompletedByDefault)
            {
                completedChecked = true;
            }
            else if (isCompletedByLevel)
            {
                if (PlayerPrefs.HasKey("LEVELCOMPLETED_" + requiredLevelID))
                {
                    completedChecked = PlayerPrefs.GetInt("LEVELCOMPLETED_" + requiredLevelID) == 1;
                }
                else
                {
                    completedChecked = false;
                }
            } 
            else if (isCompletedByOtherChecker)
            {
                if (otherChecker != null)
                {
                    bool anythingTrue = false;
                    foreach (LevelCompletedChecker checker in otherChecker)
                    {
                        if (checker.CompletedChecked)
                        {
                            anythingTrue = true;
                            break;
                        }
                    }
                    completedChecked = anythingTrue;
                }
                else
                {
                    completedChecked = false;
                }
            }
            else
            {
                completedChecked = false;
            }

            foreach (Image changedImg in changedImages)
            {
                if (completedChecked)
                {
                    changedImg.color = ActivatedColor;
                    if (changedButton != null)
                    {
                        changedButton.interactable = true;
                    }
                }
                else
                {
                    changedImg.color = DeactivatedColor;
                    if (changedButton != null)
                    {
                        changedButton.interactable = false;
                    }
                }
            }
        }
    }
}
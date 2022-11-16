using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace ChronoDivergence
{
    public class MovableBlock_break : MovableBlock
    {
        [SerializeField] private int maxMoves = -1;
        [Header("Dont change:")]
        [SerializeField] private TMP_Text maxMovesText;
        [SerializeField] private Sprite[] destructionStates;
        [SerializeField] private Collider2D collider;
        [SerializeField] private Collider2D laserBlocker;
        [SerializeField] private ShadowCaster2D shadowCaster;
        
        private void OnValidate()
        {
            if (idColor != null && idColorDisplay != null)
            {
                idColorDisplay.color = idColor;
            }

            if (maxMovesText != null)
            {
                int maxMovesInt = maxMoves - (int)movesMade;
                maxMovesText.text = maxMovesInt.ToString();
            }
            
            float statesPerMove = destructionStates.Length / maxMoves;
            int destructImageId = (int)(statesPerMove * movesMade + 1);
            if (movesMade <= 0)
            {
                destructImageId = 0;
            } else if (movesMade >= maxMoves)
            {
                destructImageId = destructionStates.Length - 1;
            }

            if (destructImageId >= 0 && destructImageId < destructionStates.Length)
            {
                mainSpriteRenderer.sprite = destructionStates[destructImageId];
            }

        }
        
        private void Update()
        {
            int maxMovesInt = maxMoves - (int)movesMade;
            maxMovesText.text = maxMovesInt.ToString();
            
            float statesPerMove = destructionStates.Length / maxMoves;
            int destructImageId = (int)(statesPerMove * movesMade + 1);
            if (movesMade <= 0)
            {
                destructImageId = 0;
            } else if (movesMade >= maxMoves)
            {
                destructImageId = destructionStates.Length - 1;
            }
            if (destructImageId >= 0 && destructImageId < destructionStates.Length)
            {
                mainSpriteRenderer.sprite = destructionStates[destructImageId];
            }
            
            if (maxMoves != -1)
            {
                if (maxMoves - movesMade <= 0)
                {
                    ExplodeBox();
                }
            }

            if (Vector2.Distance(transform.position, destination) > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, player.MoveSpeed * Time.deltaTime);
            }
            
            
        }

        private void ExplodeBox()
        {
            //TODO: Make a cool animation or something maybe?
            mainSpriteRenderer.sprite = destructionStates[destructionStates.Length - 1];
            shadowCaster.castsShadows = false;
            collider.enabled = false;
            laserBlocker.enabled = false;
            maxMovesText.enabled = false;
            idColorDisplay.enabled = false;
        }
    }
}
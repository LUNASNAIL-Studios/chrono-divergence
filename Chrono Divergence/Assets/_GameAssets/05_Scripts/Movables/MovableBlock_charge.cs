using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChronoDivergence
{
    public class MovableBlock_charge : MovableBlock
    {
        [SerializeField] private int minMoves = -1;
        [SerializeField] private Image chargeDisplay;
        [SerializeField] private Color minMovesNotReached;
        [SerializeField] private Color minMovesReached;
        
        private void OnValidate()
        {
            if (idColor != null && idColorDisplay != null)
            {
                idColorDisplay.color = idColor;
            }
            
            if (chargeDisplay != null && minMoves > 0)
            {
                chargeDisplay.color = minMovesNotReached;
                movesMade = 1;
                chargeDisplay.fillAmount = 1f / minMoves;
            }
        }
        
        new private void Update()
        {
            if (minMoves != -1)
            {
                if (movesMade < minMoves)
                {
                    chargeDisplay.fillAmount = 1f / minMoves * movesMade;
                    chargeDisplay.color = minMovesNotReached;
                }
                else
                {
                    chargeDisplay.fillAmount = 1f;
                    chargeDisplay.color = minMovesReached;
                }
            }
            
            if (Vector2.Distance(transform.position, destination) > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, player.MoveSpeed * Time.deltaTime);
            }
        }

        new public bool isLoadedEnough()
        {
            if (minMoves != -1)
            {
                return movesMade >= minMoves - 1;
            }

            return true;
        }
    }
}
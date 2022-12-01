using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChronoDivergence.Events;

namespace ChronoDivergence
{
    public class HistoryMaker : MonoBehaviour
    {
        [SerializeField] private List<Vector2> history;
        [SerializeField] GameObject objectToLog;
        private PlayerMovement player;
        private HistoryMaker playerHistory;
        private IMovable movable;

        void Start()
        {
            history = new List<Vector2>();
            history.Add(objectToLog.transform.position);
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            playerHistory = player.GetComponent<HistoryMaker>();
        }

        private void OnValidate() {
            if(objectToLog != null){
                if(objectToLog.GetInterfaces<IMovable>().Count > 0) {
                    movable = objectToLog.GetComponent<IMovable>();
                } else {
                    objectToLog = null;
                    movable = null;
                }
            }
        }

        private void OnEnable() {
            Message<PlayerMoveEvent>.Add(OnPlayerMoveEvent);
            Message<PlayerInteractEvent>.Add(OnPlayerInteractEvent);
            Message<UndoStepEvent>.Add(OnUndoStepEvent);
        }

        private void OnDisable() {
            Message<PlayerMoveEvent>.Remove(OnPlayerMoveEvent);
            Message<PlayerInteractEvent>.Remove(OnPlayerInteractEvent);
            Message<UndoStepEvent>.Remove(OnUndoStepEvent);
        }

        private void OnPlayerMoveEvent(PlayerMoveEvent e) {
            if(history.Count > 0){
                if(e.NewPosition + e.Direction != playerHistory.history[history.Count - 1]) {
                    history.Add(movable.Destination);
                }
            } else {
                history.Add(movable.Destination);
            }
        }

        private void OnPlayerInteractEvent(PlayerInteractEvent e) {
            history.Add(movable.Destination);
        }

        private void OnUndoStepEvent(UndoStepEvent e) {
            if(history.Count > 1) {
                movable.MoveInDirection(history[history.Count - 2] - history[history.Count - 1], true);
                history.RemoveAt(history.Count - 1);
            }
        }
    }
}

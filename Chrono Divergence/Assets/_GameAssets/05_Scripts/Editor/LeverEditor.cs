#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ChronoDivergence
{
    [CustomEditor(typeof(Lever))]
    public class LeverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Lever lever = (Lever)target;
            if (GUILayout.Button("↑"))
            {
                lever.Direction = Vector2.up;
                lever.UpdateDirection();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("←"))
            {
                lever.Direction = Vector2.left;
                lever.UpdateDirection();
            }
            if (GUILayout.Button("→"))
            {
                lever.Direction = Vector2.right;
                lever.UpdateDirection();
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("↓"))
            {
                lever.Direction = Vector2.down;
                lever.UpdateDirection();
            }

            DrawDefaultInspector();
        }
    }
}
#endif
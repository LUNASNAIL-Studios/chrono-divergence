#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ChronoDivergence
{
    [CustomEditor(typeof(ConveyerBelt))]
    public class ConveyerBeltEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ConveyerBelt cb = (ConveyerBelt)target;
            if (GUILayout.Button("↑"))
            {
                cb.Direction = Vector2.up;
                cb.UpdateDirection();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("←"))
            {
                cb.Direction = Vector2.left;
                cb.UpdateDirection();
            }
            if (GUILayout.Button("→"))
            {
                cb.Direction = Vector2.right;
                cb.UpdateDirection();
            }
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("↓"))
            {
                cb.Direction = Vector2.down;
                cb.UpdateDirection();
            }

            DrawDefaultInspector();
        }
    }
}
#endif
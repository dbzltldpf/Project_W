#if UNITY_EDITOR
namespace W
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;

    [CustomEditor(typeof(ComUITools))]
    public class SpawnInCircle : Editor
    {
        private List<Vector2Int> toolKeys = new();

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Editor Settings", EditorStyles.boldLabel);

            // 실행 버튼
            if (GUILayout.Button("Spawn Tools In Circle"))
            {
                ComUITools tools = (ComUITools)target;
                var distance = tools.Distance;
                var toolCount = tools.transform.childCount;
                SetPositionInCircle(tools.transform, toolCount, distance);
                EditorUtility.SetDirty(tools);
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Editor Actions", EditorStyles.boldLabel);

            EditorGUILayout.Space(10);
            base.OnInspectorGUI();
        }

        private void SetPositionInCircle(Transform parent, int toolCount, float distance)
        {
            float baseAngle = 360f / toolCount;

            for(int i = 0; i < toolCount; ++i)
            {
                float angle = 90 - i * baseAngle;   // ↑ 시작, 시계방향
                float rad = angle * Mathf.Deg2Rad;

                var pos = new Vector2(
                    Mathf.Cos(rad) * distance,
                    Mathf.Sin(rad) * distance
                );

                var toolRT = parent.GetChild(i).GetComponent<RectTransform>();
                toolRT.anchoredPosition = pos;
            }
        }
    }
}
#endif
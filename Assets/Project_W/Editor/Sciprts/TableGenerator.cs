using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

public class TableGenerator : EditorWindow
{
    private Object droppedFolder; // 드래그된 폴더
    private string folderPath;    // 폴더의 실제 경로

    private const string OUTPUT_PATH = "Assets/Project_W/Runtime/Scripts/Data/ItemID.cs";

    List<KeyValuePair<string, int>> containers = new();


    [MenuItem("Tools/Table Generator")]
    public static void ShowWindow()
    {
        GetWindow<TableGenerator>("Table Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Table Generator", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // 버튼 (폴더가 선택되어야 활성화)
        using (new EditorGUI.DisabledScope(string.IsNullOrEmpty(folderPath)))
        {
            if (GUILayout.Button("Generate", GUILayout.Height(30)))
            {
                Generate(folderPath);
                CreateCode();
            }
        }

        GUILayout.Space(15);
        GUILayout.Label("폴더를 여기에 드래그하세요:", EditorStyles.label);

        // 드래그 드랍 영역
        Rect dropArea = GUILayoutUtility.GetRect(0, 60, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag & Drop Folder Here", EditorStyles.helpBox);

        Event evt = Event.current;
        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    break;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object obj in DragAndDrop.objectReferences)
                    {
                        string path = AssetDatabase.GetAssetPath(obj);
                        if (Directory.Exists(path))
                        {
                            droppedFolder = obj;
                            folderPath = path;
                            Debug.Log($"폴더 드롭됨: {path}");
                        }
                        else
                        {
                            Debug.LogWarning($"'{path}' 는 폴더가 아닙니다.");
                        }
                    }
                }
                Event.current.Use();
                break;
        }

        GUILayout.Space(10);

        if (droppedFolder != null)
        {
            EditorGUILayout.ObjectField("드롭된 폴더", droppedFolder, typeof(Object), false);
        }
    }

    private void Generate(string targetFolder)
    {
        if (string.IsNullOrEmpty(targetFolder) || !Directory.Exists(targetFolder))
        {
            Debug.LogWarning("유효하지 않은 폴더입니다.");
            return;
        }

        string[] jsonFiles = Directory.GetFiles(targetFolder, "*.json", SearchOption.TopDirectoryOnly);

        if (jsonFiles.Length == 0)
        {
            Debug.LogWarning("해당 폴더에 JSON 파일이 없습니다.");
            return;
        }

        Debug.Log($"총 {jsonFiles.Length}개의 JSON 파일 발견됨:");
        ProcessJsonFiles(jsonFiles);
    }

    private void ProcessJsonFiles(IEnumerable<string> jsonFilePaths)
    {
        foreach (var path in jsonFilePaths)
        {
            try
            {
                string json = File.ReadAllText(path).Trim();
                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogWarning($"빈 파일: {path}");
                    continue;
                }

                var matches = Regex.Matches(json, @"\{[^{}]*\}");
                List<string> items = new List<string>();
                foreach (Match match in matches)
                {
                    string item = match.Value;
                    item = Regex.Replace(item, "[\"{}]", "");
                    items.Add(item);
                }

                for (int i = 0; i < items.Count; ++i)
                {
                    var splits = items[i].Split(',');
                    var index = int.Parse(splits[0].Split(':')[1]);
                    var name = splits[1].Split(':')[1];
                    name = name.Replace(" ", "_");

                    containers.Add(new KeyValuePair<string, int>(name, index));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[TableGenerator] 예외 발생 ({path}): {ex.Message}");
            }
        }
    }

    private void CreateCode()
    {
        var orderedContainers = containers.OrderBy(item => item.Value);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("public enum ItemID");
        sb.AppendLine("{");

        foreach(var item in orderedContainers)
        {
            sb.AppendLine($"    {item.Key} = {item.Value},");
        }

        sb.AppendLine("}");

        Write(sb);
    }

    private void Write(StringBuilder sb)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(OUTPUT_PATH)!);
        File.WriteAllText(OUTPUT_PATH, sb.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh();

        UnityEngine.Debug.Log($"✅ TableGenerator.cs regenerated at: {OUTPUT_PATH}");
    }
}

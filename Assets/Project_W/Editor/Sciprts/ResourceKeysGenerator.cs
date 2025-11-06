#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

public static class ResourceKeysGenerator
{
    private const string OUTPUT_PATH = "Assets/Project_W/Runtime/Scripts/Data/ResourceKeys.cs";
    private const string ROOT_NAMESPACE = "W";

    // 프로젝트에서 Resource 루트(사용자 지정)에 맞춰 변경 가능
    private const string RESOURCE_ROOT = "Assets/Project_W/Runtime/Resource/";

    [MenuItem("Tools/Generate ResourceKeys")]
    public static void GenerateKeys()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            UnityEngine.Debug.LogError("AddressableAssetSettings not found!");
            return;
        }

        // 수집: Addressable entries 중에서 실제 assetPath가 RESOURCE_ROOT을 포함하는 것만 처리
        var entries = new List<AddressableAssetEntry>();
        foreach (var g in settings.groups)
        {
            if (g == null) continue;
            foreach (var e in g.entries)
            {
                var path = AssetDatabase.GUIDToAssetPath(e.guid);
                if (string.IsNullOrEmpty(path)) continue;
                if (!path.StartsWith(RESOURCE_ROOT)) continue;
                entries.Add(e);
            }
        }

        if (entries.Count == 0)
        {
            UnityEngine.Debug.LogWarning("No addressable entries under RESOURCE_ROOT found.");
            return;
        }

        // 트리 구조: groupFolder -> subFolder(예: Inventory) -> list of assetPaths
        var tree = new Dictionary<string, Dictionary<string, List<string>>>();

        foreach (var entry in entries)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(entry.guid);
            var rel = assetPath.Substring(RESOURCE_ROOT.Length); // UI_Common/Inventory/Asset/xxx.png
            var parts = rel.Split('/');

            if (parts.Length < 2)
            {
                // 최소 구조(UIGroup/Folder/Subfolder/file)를 만족하지 않으면 무시
                UnityEngine.Debug.LogWarning($"Skipping unexpected path structure: {assetPath}");
                continue;
            }

            string groupFolder = parts[0];   // ex: UI_Common
            string folderName = parts[1];    // ex: Inventory

            if (!tree.ContainsKey(groupFolder))
                tree[groupFolder] = new Dictionary<string, List<string>>();

            if (!tree[groupFolder].ContainsKey(folderName))
                tree[groupFolder][folderName] = new List<string>();

            // 서브 폴더 탐색 // ex: Asset, CacheReference, Staticreference
            foreach (var subAsset in entry.SubAssets)
            {
                if (!subAsset.IsFolder)
                    continue;

                string subFolder = subAsset.address.Split('/')[^1];
                if (string.Equals(subFolder, "StaticReference", System.StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (var asset in subAsset.SubAssets)
                {
                    if (!tree[groupFolder][folderName].Contains(asset.address))
                        tree[groupFolder][folderName].Add(asset.address);
                }
            }
        }

        // 코드 생성.
        var sb = new StringBuilder();
        sb.AppendLine($"namespace {ROOT_NAMESPACE}");
        sb.AppendLine("{");
        sb.AppendLine("    public static class ResourceKeys");
        sb.AppendLine("    {");

        // 기본(Default_Local_Group) 빈 클래스 유지.
        sb.AppendLine("        public static class Default_Local_Group");
        sb.AppendLine("        {");
        sb.AppendLine("        }");
        sb.AppendLine();

        foreach (var group in tree)
        {
            string folderClass = SanitizeName(group.Key);
            sb.AppendLine($"        public static class {folderClass}");
            sb.AppendLine("        {");

            foreach (var subGroup in group.Value)
            {
                string subClass = SanitizeName(subGroup.Key);
                sb.AppendLine($"            public static class {subClass}");
                sb.AppendLine("            {");

                foreach (var asset in subGroup.Value)
                {
                    string baseName = asset.Split('/')[^1];     // Inventory.prefab
                    string[] parts = baseName.Split('.');       // Inventory, prefab
                    string constName = parts[0];                // Inventory
                    if (baseName.Contains("prefab"))
                        constName += "Prefab";                  // InventoryPrefab
                    sb.AppendLine($"                public const string {constName} = \"{asset}\";");
                }

                sb.AppendLine("            }");
            }

            sb.AppendLine("        }");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");

        Write(sb);
    }

    private static void Write(StringBuilder sb)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(OUTPUT_PATH)!);
        File.WriteAllText(OUTPUT_PATH, sb.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh();

        UnityEngine.Debug.Log($"✅ ResourceKeys.cs regenerated at: {OUTPUT_PATH}");
    }

    // -------------------- 유틸 --------------------

    private static string SanitizeName(string name)
    {
        if (string.IsNullOrEmpty(name)) return "Unknown";
        var clean = new string(name.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());
        if (char.IsDigit(clean.FirstOrDefault())) clean = "_" + clean;
        return clean;
    }

    private static string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return "Unknown";
        var words = input.Split(new[] { '_', '-', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(w => char.ToUpper(w[0], CultureInfo.InvariantCulture) + (w.Length > 1 ? w.Substring(1) : "")));
    }
}
#endif

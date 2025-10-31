using System.Collections.Generic;

public class FolderNode
{
    public string Name;
    public Dictionary<string, FolderNode> Children = new();
    public List<string> Files = new();

    public FolderNode(string name) => Name = name;

    public void AddPath(string fullPath)
    {
        var parts = fullPath.Split('/', '\\');
        AddRecursive(parts, 0);
    }

    private void AddRecursive(string[] parts, int index)
    {
        if(index == parts.Length - 1)
        {
            Files.Add(parts[index]);
            return;
        }

        var dir = parts[index];
        if (!Children.ContainsKey(dir))
            Children[dir] = new FolderNode(dir);

        Children[dir].AddRecursive(parts, index + 1);
    }
}

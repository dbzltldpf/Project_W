namespace W
{
    using UnityEngine;
    using System.Collections.Generic;

    public class ToolManager : Singleton<ToolManager>
    {
        private readonly Dictionary<Vector2Int, ToolType> directions = new Dictionary<Vector2Int, ToolType>
        {
            { new Vector2Int(0, 1),     ToolType.Hand },
            { new Vector2Int(1, 1),     ToolType.Nest },
            { new Vector2Int(1, 0),     ToolType.Pickax },
            { new Vector2Int(1, -1),    ToolType.Trowel },
            { new Vector2Int(0, -1),    ToolType.Pen },
            { new Vector2Int(-1, -1),   ToolType.Rod },
            { new Vector2Int(-1, 0),    ToolType.x1 },
            { new Vector2Int(-1, 1),    ToolType.x2 }
        };
       private Dictionary<ToolType, ToolBase> tools = new();
        private ToolBase currentTool = null;

        public ToolBase Tool => currentTool;

        public void Regist(ToolType type, ToolBase tool)
        {
            tools[type] = tool;
        }

        public ToolBase EquipTool(Vector2 direction)
        {
            return EquipTool(new Vector2Int((int)direction.x, (int)direction.y));
        }

        public ToolBase EquipTool(Vector2Int direction)
        {
            if(directions.TryGetValue(direction, out var toolType))
            {
                if(tools.TryGetValue(toolType, out var tool))
                {
                    if (currentTool == tool) return currentTool;

                    currentTool = tool;

                    Debug.Log($"{toolType} 도구 장착.");
                }
                else 
                {
                    Debug.LogWarning($"[ToolManager] {toolType} key is not defined.");
                }
            }
            else
            {
                Debug.LogWarning($"[ToolManager] {direction} key is not defined.");
            }

            return currentTool;
        }
    }
}
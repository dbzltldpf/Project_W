//TODO: TEST DATA
namespace S
{
    using UnityEngine;
    public class TestData
    { }
    public class ToolData
    {
        public string toolName;
        public Sprite icon;
        public InteractType interactType;
        public ToolData(string toolName, InteractType interactType)
        {
            this.toolName = toolName;
            this.interactType = interactType;
        }
    }
    public enum PotionType
    {
        Drink,
        Throw,
    }
    public class PotionData
    {
        public string name;
        public PotionType pointerType;
        public PotionData(string name, PotionType type)
        {
            this.name = name;
            this.pointerType = type;
        }
    }

}

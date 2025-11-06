namespace W
{
    using UnityEngine;

    public class ToolTest : MonoBehaviour
    {
        public Vector2 direction = Vector2.zero;

        private void Start()
        {
            ToolManager.Instance.EquipTool(Vector2.up);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
                ToolManager.Instance.EquipTool(direction);

            if (Input.GetKeyUp(KeyCode.Space))
                ToolManager.Instance.Tool.Use();
        }
    }
}
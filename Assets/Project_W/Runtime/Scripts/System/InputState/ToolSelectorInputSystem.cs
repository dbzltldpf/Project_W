namespace S
{
    using ToolManager = W.ToolManager;
    using UnityEditor.EditorTools;
    using UnityEngine;
    using W;

    public class ToolSelectorInputSystem : IInputState
    {
        Vector2 currentDir;
        public void Enter()
        {
            W.UIManager.Instance.Open(UGuiId.Tools);
        }
        public void HandleInput(InputContext inputContext)
        {
            var input = inputContext.input;
            Vector2 dir = input.GetMoveInput();
            if(dir != Vector2.zero)
            {
                currentDir = dir;
            }

            //ui쪽 움직임
            //dir로 커서 이동
            if (input.InteractPressed())
            {
                //ui 활성화 된 부분 선택
                Debug.Log("선택&닫기");
                input.ChangeState(new DefaultInputSystem());
                ToolBase currentTool = ToolManager.Instance.EquipTool(currentDir);
                inputContext.player.SelectTool(currentTool);
                W.UIManager.Instance.Close(UGuiId.Tools);
            }
            if (input.CancelPressed())
            {
                //ui 끄기
                Debug.Log("닫기");
                input.ChangeState(new DefaultInputSystem());
                W.UIManager.Instance.Close(UGuiId.Tools);
            }

        }
    }
}

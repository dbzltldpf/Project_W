namespace S
{
    using UnityEngine;
    public class ToolSelectorInputSystem : IInputState
    {
        Vector2 currentDir;
        public void Enter()
        {
            UIManager.Instance.ShowUI(UIManager.Instance.toolSelector);
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
                inputContext.player.SelectTool(SelectTool(currentDir));
                //활성화 된 도구 선택
                UIManager.Instance.HideUI(UIManager.Instance.toolSelector);
            }
            if (input.CancelPressed())
            {
                //ui 끄기
                Debug.Log("닫기");
                input.ChangeState(new DefaultInputSystem());
                UIManager.Instance.HideUI(UIManager.Instance.toolSelector);
            }

        }

        //TODO: 테스트용

        public ToolData SelectTool(Vector2 dir)
        {
            var toolData = new ToolData(null, default);
            switch(dir.x,dir.y)
            {
                case (0, 1):
                    toolData = new ToolData("손", InteractType.Gather);
                    break;
                case (0.7f, 0.7f):
                    break;
                case (1, 0):
                    break;
                case (0.7f, -0.7f):
                    break;
                case (0, -1):
                    toolData = new ToolData("깃털", InteractType.Draw);
                    break;
                case (-0.7f, -0.7f):
                    break;
                case (-1, 0):
                    break;
                case (-0.7f, 0.7f):
                    break;
            }
            //손, 포충망, 곡괭이, 삽, 깃털
            return toolData;
        }
    }
}

namespace S
{
    using UnityEngine;
    public class TooSelectorInputSystem : IInputState
    {
        public void HandleInput(InputContext inputContext)
        {
            var input = inputContext.input;
            Vector2 dir = input.GetMoveInput();
            //ui쪽 움직임
            //dir로 커서 이동
            if(input.InteractPressed())
            {
                //ui 활성화 된 부분 선택
            }
            if(input.CancelPressed())
            {
                //ui 끄기
            }

        }

    }
}

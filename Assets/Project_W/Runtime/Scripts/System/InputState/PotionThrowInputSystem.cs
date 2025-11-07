namespace S
{
    using UnityEngine;
    public class PotionThrowInputSystem : IInputState
    {
        public bool ShouldStopOnEnter => false;
        bool isInputLocked;
        private Vector2 aimDir = Vector2.zero;
        private float throwDistance = 2f;
        bool isAiming;
        public void Enter()
        {
            UIManager.Instance.ShowUI(UIManager.Instance.potionThrow);
            isInputLocked = true;
            isAiming = false;
        }

        public void HandleInput(InputContext inputContext)
        {
            var input = inputContext.input;
            var player = inputContext.player;
            Vector2 dir = input.GetMoveInput();

            //z누르는 상태에서 화살표로 위치 조절, z up 발사
            if (input.IsInteractPressed())
            {
                if (isInputLocked)
                    return;

                //위치조절
                Debug.Log("조준");
                //조준 작동 해야함
            }
            if (input.InteractReleased())
            {
                if (!isInputLocked)
                {
                    Debug.Log("던지기");
                    //포션이 날라가는 애니
                    player.ChangeState(player.state.Throw);
                    UIManager.Instance.HideUI(UIManager.Instance.potionThrow);
                }
                else
                {
                    isInputLocked = false;
                }
            }
            if (input.CancelPressed())
            {
                Debug.Log("닫기");
                input.ChangeState(new DefaultInputSystem());
                UIManager.Instance.HideUI(UIManager.Instance.potionThrow);
            }
        }

    }
}

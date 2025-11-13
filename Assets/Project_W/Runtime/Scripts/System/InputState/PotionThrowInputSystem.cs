namespace S
{
    using UnityEngine;
    public class PotionThrowInputSystem : IInputState
    {
        public bool ShouldStopOnEnter => false;
        bool isInputLocked;
        bool isAiming = false;
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
            Vector2 moveInput = input.GetMoveInput();

            if(input.InteractPressed())
            {
                if (isInputLocked)
                    return;
                isAiming = true;
                Debug.Log("조준 시작");
                player.ChangeState(player.state.ThrowReady);
            }
            //z누르는 상태에서 화살표로 위치 조절, z up 발사
            if (input.IsInteractPressed())
            {
                if (!isAiming)
                    return;

                player.Move(moveInput, false, false);

                //조준 작동 해야함
                player.UpdateAim(player.dir);
            }
            if (input.InteractReleased())
            {
                if (isAiming)
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
                Debug.Log("닫기&조준취소");
                input.ChangeState(new DefaultInputSystem());
                UIManager.Instance.HideUI(UIManager.Instance.potionThrow);
            }
        }

    }
}

namespace S
{
    using UnityEngine;
    public class PotionSelectorInputSystem : IInputState
    {
        public void Enter()
        {
            Debug.Log("포션 선택 화면");
            UIManager.Instance.ShowUI(UIManager.Instance.potionSelector);
        }

        public void HandleInput(InputContext inputContext)
        {
            var input = inputContext.input;
            var player = inputContext.player;
            Vector2 dir = input.GetMoveInput();

            if(input.InteractPressed())
            {
                PotionData selectedPotion = new PotionData("x", PotionType.Throw);
                //선택
                //어떤 포션 인지 확인
                if (selectedPotion.name == null)
                {
                    Debug.Log("물약이 존재하지않음");
                    return;
                }

                switch (selectedPotion.pointerType)
                {
                    case PotionType.Throw:
                        input.ChangeState(new PotionThrowInputSystem());
                        break;
                    case PotionType.Drink:
                        //물약마시는
                        player.ChangeState(player.state.Drink);
                        break;
                }
                
                UIManager.Instance.HideUI(UIManager.Instance.potionSelector);
            }
            if (input.CancelPressed())
            {
                //ui 끄기
                Debug.Log("닫기");
                input.ChangeState(new DefaultInputSystem());
                UIManager.Instance.HideUI(UIManager.Instance.potionSelector);
            }

        }
    }
}

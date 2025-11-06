namespace S
{
    using UnityEngine;

    public class DefaultInputSystem : IInputState
    {
        public bool ShouldStopOnEnter => false;

        public void Enter()
        {
        }

        public void HandleInput(InputContext inputContext)
        {
            var player = inputContext.player;
            var input = inputContext.input;
            
            Vector2 moveInput = input.GetMoveInput();
            bool isRun = input.IsRunPressed();
            bool isWalk = input.IsWalkPressed();
            player.Move(moveInput, isRun, isWalk);

            if(input.ToolSelectorPressed())
            {
                input.ChangeState(new ToolSelectorInputSystem());
            }

            if (input.InteractPressed())
            {
                player.TryInterect();
            }

            if(input.PotionSelectorPressed())
            {
                input.ChangeState(new PotionSelectorInputSystem());
            }

        }
    }
}

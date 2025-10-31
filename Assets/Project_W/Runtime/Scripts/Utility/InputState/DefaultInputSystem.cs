namespace S
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class DefaultInputSystem : IInputState
    {
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
                input.ChangeState(new TooSelectorInputSystem());
            }

            if (input.InteractPressed())
            {
                player.TryInterect();
            }

        }

    }
}


namespace S
{
    using UnityEngine;
    public class PlayerMoveState : ICharacterState
    {
        private PlayerController player;

        public PlayerMoveState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            Debug.Log("sss");
            player.SetRun(true);
        }

        public void Exit()
        {
        }
        public void FixedUpdate()
        {
            player.rb.linearVelocity = player.MoveInput * player.currentSpeed;
            player.animator.SetFloat("MoveX", player.MoveInput.x);
            player.animator.SetFloat("MoveY", player.MoveInput.y);

            player.animator.speed = player.IsWalk ? 0.5f : player.IsRun ? 2f : 1f;

            if(player.MoveInput != Vector2.zero)
            {
                player.dir = player.MoveInput;
            }
        }

        public void Update()
        {
        }
    }

}

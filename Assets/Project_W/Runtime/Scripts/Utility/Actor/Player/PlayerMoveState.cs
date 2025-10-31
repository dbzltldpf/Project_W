
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
            player.animator.SetBool("isMove", player.isMoving);
        }

        public void Exit()
        {
        }
        public void FixedUpdate()
        {
            player.rb.linearVelocity = player.moveInput * player.currentSpeed;
            player.animator.SetFloat("MoveX", player.moveInput.x);
            player.animator.SetFloat("MoveY", player.moveInput.y);

            if(!player.isWalk)
                player.animator.speed = player.currentSpeed;
            else
                player.animator.speed = player.moveSpeed;

            player.animator.SetBool("isWalk", player.isWalk);

            if(player.moveInput != Vector2.zero)
            {
                player.dir = player.moveInput;
            }
        }

        public void Update()
        {
        }
    }

}

namespace S
{
    using UnityEngine;

    public class PlayerIdleState : ICharacterState
    {
        private PlayerController player;
        public PlayerIdleState(PlayerController player)
        {
            this.player = player;
        }
        public void Enter()
        {
            player.animator.SetBool("isMove", player.isMoving);

            player.isRun = false;
            player.animator.speed = 1f;
            player.isWalk = false;
            player.animator.SetBool("isWalk", player.isWalk);

            player.animator.SetFloat("LastMoveX", player.dir.x);
            player.animator.SetFloat("LastMoveY", player.dir.y);
        }

        public void Exit()
        {
        }

        public void FixedUpdate()
        {
            player.rb.linearVelocity = Vector2.zero;
        }

        public void Update()
        {
        }
    }
}

namespace S
{
    using UnityEngine;

    public class PlayerThrowReadyState : ICharacterState
    {
        private PlayerController player;
        private Vector2 lockDir;
        public PlayerThrowReadyState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            lockDir = player.moveInput != Vector2.zero? player.moveInput : player.dir;
            player.animator.speed = 1f;
            player.animator.SetBool("isWalk", false);
            player.lockDirection = true;
            //조준선 UI 켜기
        }

        public void Exit()
        {
            player.lockDirection = false;
            //조준선 UI 끄기
        }

        public void FixedUpdate()
        {
            //가만히 서 있을때랑 움직일때 체크 해야함
            player.rb.linearVelocity = player.moveInput * player.currentSpeed;
            player.animator.SetFloat("MoveX", lockDir.x);
            player.animator.SetFloat("MoveY", lockDir.y);
        }

        public void Update()
        {
            player.GetPotionStartPos();
        }
    }
}

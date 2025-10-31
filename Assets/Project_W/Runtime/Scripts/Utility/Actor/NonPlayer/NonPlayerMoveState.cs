namespace S
{
    using UnityEngine;

    //NPC는 타일맵으로 이동, Creature는 포지션으로 이동
    public class NonPlayerMoveState : ICharacterState
    {
        private NonPlayerController nonPlayer;
        public NonPlayerMoveState(NonPlayerController nonPlayer)
        {
            this.nonPlayer = nonPlayer;
        }

        public void Enter()
        {
            nonPlayer.SetRandomTarget();
            nonPlayer.animator.SetBool("isMove", nonPlayer.isMoving);
        }

        public void Exit()
        {
        }

        public void FixedUpdate()
        {
            Vector2 dir = (nonPlayer.targetPos - nonPlayer.rb.position).normalized;
            float distance = Vector2.Distance(nonPlayer.rb.position, nonPlayer.targetPos);
            if (distance > 0.1f)
            {
                nonPlayer.rb.MovePosition(nonPlayer.rb.position + dir * nonPlayer.currentSpeed * Time.fixedDeltaTime);
                nonPlayer.animator.SetFloat("MoveX", dir.x);
                nonPlayer.animator.SetFloat("MoveY", dir.y);
                nonPlayer.dir = dir;
            }
            else 
            {
                nonPlayer.isMoving = false;
            }
        }

        public void Update()
        {
           
        }
    }
}

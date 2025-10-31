namespace S
{
    using UnityEngine;

    public class NonPlayerIdleState : ICharacterState
    {
        private NonPlayerController nonPlayer;
        public NonPlayerIdleState(NonPlayerController nonPlayer)
        {
            this.nonPlayer = nonPlayer;
        }


        public void Enter()
        {
            nonPlayer.animator.SetBool("isMove", nonPlayer.isMoving);

            nonPlayer.animator.SetFloat("LastMoveX", nonPlayer.dir.x);
            nonPlayer.animator.SetFloat("LastMoveY", nonPlayer.dir.y);
        }

        public void Exit()
        {
        }

        public void FixedUpdate()
        {

        }

        public void Update()
        {
            
        }
    }
}

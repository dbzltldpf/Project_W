namespace S
{
    using UnityEngine;

    public class PlayerThrowState : ICharacterState
    {
        private PlayerController player;
        public PlayerThrowState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            //던지는 애니
            player.animator.SetBool("isThrow", true);
            Debug.Log(player.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        }

        public void Exit()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            Debug.Log(player.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

            if (AniStateCheck.IsPlaying(player.animator, "ThrowBlendTree"))
            {
                Debug.Log("ddd");
                player.animator.SetBool("isThrow", false);
                player.ChangeState(player.state.Idle);
            }
        }
    }
}

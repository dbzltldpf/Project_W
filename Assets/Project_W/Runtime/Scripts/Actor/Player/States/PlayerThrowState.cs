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
            player.potionIndicator.Hide();
            player.animator.SetBool("isThrow", true);
        }

        public void Exit()
        {
            player.animator.SetBool("isThrow", false);
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

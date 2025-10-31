namespace S
{
    public class PlayerDrawState : ICharacterState
    {
        private PlayerController player;
        public PlayerDrawState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            player.animator.Play("SketchBlendTree", 0, 0);
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

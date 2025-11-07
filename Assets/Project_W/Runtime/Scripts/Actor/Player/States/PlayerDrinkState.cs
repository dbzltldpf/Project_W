namespace S
{
    public class PlayerDrinkState : ICharacterState
    {
        private PlayerController player;
        public PlayerDrinkState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            //마시는 애니
            player.animator.SetBool("isDrink", true);
        }

        public void Exit()
        {
            player.animator.SetBool("isDrink", false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {

        }
    }
}

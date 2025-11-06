namespace S
{
    using System.Collections.Generic;

    public class PlayerStates
    {
        public ICharacterState Idle { get; private set; }
        public ICharacterState Move { get; private set; }
        public ICharacterState Talk { get; private set; }
        public ICharacterState Gather { get; private set; }
        public ICharacterState Draw { get; private set; }
        public ICharacterState Throw { get; private set; }
        public ICharacterState Drink { get; private set; }
        public PlayerStates(PlayerController player)
        {
            Idle = new PlayerIdleState(player);
            Move = new PlayerMoveState(player);
            Talk = new PlayerTalkState(player);
            Gather = new PlayerGatherState(player);
            Draw = new PlayerDrawState(player);
            Throw = new PlayerThrowState(player);
            Drink = new PlayerDrinkState(player);
        }
        public IEnumerable<ICharacterState> GetAll()
        {
            return new ICharacterState[] { Idle, Move, Talk, Gather, Draw, Throw, Drink };
        }
    }
}

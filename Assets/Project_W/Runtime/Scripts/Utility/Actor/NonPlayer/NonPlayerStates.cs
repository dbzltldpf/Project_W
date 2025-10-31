namespace S
{
    using System.Collections.Generic;

    public class NonPlayerStates
    {
        public ICharacterState Idle {  get; private set; }
        public ICharacterState Move { get; private set; }
        public ICharacterState Talk { get; private set; }
        public NonPlayerStates(NonPlayerController nonPlayer)
        {
            Idle = new NonPlayerIdleState(nonPlayer);
            Move = new NonPlayerMoveState(nonPlayer);
            Talk = new NonPlayerTalkState(nonPlayer);
        }
        public IEnumerable<ICharacterState> GetAll()
        { 
            return new ICharacterState[] { Idle, Move, Talk };
        }
    }
}

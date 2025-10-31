namespace S
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    public struct InputContext
    {
        public PlayerController player;
        public InputSystem input;
        //uiManager 받기
        public InputContext(PlayerController player, InputSystem inputSystem)
        {
            this.player = player;
            this.input = inputSystem;
        }
    }
    public interface IInputState
    {
        void HandleInput(InputContext inputContext);
    }
    public class InputStateMachine : MonoBehaviour
    {
        IInputState currentState;
        Dictionary<Type, IInputState> states = new Dictionary<Type, IInputState>();
        public void AddState(IInputState state)
        {
            var type = state.GetType();
            if (!states.ContainsKey(type))
                states[type] = state;
        }
        public void ChangeState(Type stateType)
        {
            if (currentState != null && currentState.GetType() == stateType)
                return;

            if (!states.ContainsKey(stateType))
                return;
            //전 상태 exit함수 실행
            currentState = states[stateType];
        }
        public void HandleInput(InputContext inputContext)
        {
            currentState.HandleInput(inputContext);
        }
    }

}

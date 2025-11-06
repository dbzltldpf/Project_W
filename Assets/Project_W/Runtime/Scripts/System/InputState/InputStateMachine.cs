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
        bool ShouldStopOnEnter => true;
        void Enter();
        void HandleInput(InputContext inputContext);
    }
    public class InputStateMachine
    {
        IInputState currentState;
        Dictionary<Type, IInputState> states = new Dictionary<Type, IInputState>();
        public void AddState(IInputState state)
        {
            var type = state.GetType();
            if (!states.ContainsKey(type))
                states[type] = state;
        }
        public void ChangeState(Type stateType,InputSystem input)
        {
            if (currentState != null && currentState.GetType() == stateType)
                return;

            if (!states.ContainsKey(stateType))
                return;

            currentState = states[stateType];
            if (currentState.ShouldStopOnEnter)
            {
                input.player.Move(Vector2.zero, false, false);
            }
            currentState?.Enter();
        }
        public void HandleInput(InputContext inputContext)
        {
            if(currentState == null)
            {
                Debug.Log("currentState null");
                return;
            }
            currentState.HandleInput(inputContext);
        }
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;
public interface ICharacterState
{
    void Enter();
    void Update();
    void FixedUpdate();
    void Exit();
}
public class CharacterStateMachine
{
    ICharacterState currentState;
    Dictionary<Type,ICharacterState> states = new Dictionary<Type, ICharacterState> ();
    public void AddState(ICharacterState state)
    {
        var type = state.GetType();
        if (!states.ContainsKey(type))
            states[type] = state;
    }
    public void ChangeState(Type stateType)
    {
        if(currentState != null && currentState.GetType() == stateType)
            return;

        if (!states.ContainsKey(stateType))
            return;
        //전 상태 exit함수 실행
        currentState?.Exit();
        currentState = states[stateType];
        currentState?.Enter();
    }
    public void Update()
    {
        currentState?.Update();
    }
    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}

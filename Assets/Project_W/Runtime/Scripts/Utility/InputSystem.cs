﻿namespace S
{
    using UnityEngine;

    public class InputSystem : Singleton<InputSystem>
    {
        public InputBinding _binding;
        private InputStateMachine stateMachine;
        private PlayerController player;

        public void Awake()
        {
            stateMachine = new InputStateMachine();
            var inputStates = new InputStates();
            foreach (var state in inputStates.GetAll())
            {
                stateMachine.AddState(state);
            }
        }
        public void Initialize(PlayerController player)
        {
            this.player = player;
            ChangeState(new DefaultInputSystem());
        }
        public void ChangeState(IInputState newState)
        {
            stateMachine.ChangeState(newState.GetType());
        }
        public void Update()
        {
            var inputContext = new InputContext(player, this);
            stateMachine.HandleInput(inputContext);
        }
        public Vector2 GetMoveInput()
        {
            Vector2 dir = Vector2.zero;
            if(Input.GetKey(_binding._bindingDict[UserAction.MoveForward]))
            {
                dir += Vector2.up;
            }
            if (Input.GetKey(_binding._bindingDict[UserAction.MoveBackward]))
            {
                dir += Vector2.down;
            }
            if (Input.GetKey(_binding._bindingDict[UserAction.MoveLeft]))
            {
                dir += Vector2.left;
            }
            if (Input.GetKey(_binding._bindingDict[UserAction.MoveRight]))
            {
                dir += Vector2.right;
            }

            if (dir.sqrMagnitude > 1f)
                dir.Normalize();

            return dir;

        }
        public bool IsRunPressed()
        {
            if (TryGetKey(UserAction.Run, out var key))
                return Input.GetKey(key);
            return false;
        }
        public bool IsWalkPressed()
        {
            if (TryGetKey(UserAction.Walk, out var key))
                return Input.GetKey(key);
            return false;
        }

        public bool InteractPressed()
        {
            if (TryGetKey(UserAction.Interact, out var key))
                return Input.GetKeyDown(key);
            return false;
        }
        public bool CancelPressed()
        {
            if (TryGetKey(UserAction.Cancel, out var key))
                return Input.GetKeyDown(key);
            return false;
        }
        public bool ToolSelectorPressed()
        {
            if (TryGetKey(UserAction.ToolSelector, out var key))
                return Input.GetKeyDown(key);
            return false;
        }
        //키 바인딩이 none일때 방지
        private bool TryGetKey(UserAction action, out KeyCode key)
        {
            if(!_binding._bindingDict.ContainsKey(action))
            {
                key = KeyCode.None;
                return false;
            }
            key = _binding._bindingDict[action];
            return key != KeyCode.None;
        }
    }
}

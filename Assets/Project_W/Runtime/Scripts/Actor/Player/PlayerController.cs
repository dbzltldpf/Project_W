namespace S
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using W;
    using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

    public enum InteractType
    {
        Talk,
        Gather,
        Draw
    }
    public class PlayerController : Actor
    {
        public Vector2 moveInput;
        public PlayerStates state;
        public bool isRun = false;
        public bool isWalk = false;
        public bool canInterect = false;

        public IInteractable currentTarget;
        public IInteractionStrategy currentStrategy;
        public ToolType toolType;
        public override void Awake()
        {
            base.Awake();            
            state = new PlayerStates(this);
            currentStrategy = new DrawInteraction();

            foreach(var state in state.GetAll())
                stateMachine.AddState(state);
        }
        public override void Start()
        {
            base.Start();
            ChangeState(state.Idle);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        public override void Update()
        {
            base.Update();
        }
        public void Move(Vector2 moveInput, bool isRun, bool isWalk)
        {
            this.moveInput = moveInput;
            isMoving = moveInput != Vector2.zero;
            currentSpeed = isWalk ? walkSpeed : isRun ? runSpeed : moveSpeed;
            ChangeState(isMoving ? state.Move : state.Idle);
        }
        public void TryInterect()
        {
            if (!canInterect || currentTarget == null)
                return;

            currentStrategy?.Interact(this, currentTarget);
        }
        public void SetInteractionStrategy(IInteractionStrategy strategy)
        {
            currentStrategy = strategy;
        }
        public void SelectTool(ToolBase tool)
        {
            if (tool == null)
                return;

            toolType = tool.Type;
            switch(toolType)
            {
                case ToolType.Hand:
                    SetInteractionStrategy(new TalkInteraction());
                    break;
                case ToolType.Nest:
                    SetInteractionStrategy(new GatherInteraction());
                    break;
                case ToolType.Pen:
                    SetInteractionStrategy(new DrawInteraction());
                    break;
            }
        }
        public void Check()
        {
            
        }
    }

}

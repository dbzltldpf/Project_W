namespace S
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public enum InteractType
    {
        Talk,
        Gather,
        Draw
    }
    public class PlayerController : Actor
    {
        public Vector2 moveInput;
        public InteractType interactType;

        public PlayerStates state;
        public bool isRun = false;
        public bool isWalk = false;
        public bool canInterect = false;

        public IInteractable currentTarget;
        public IInteractionStrategy currentStrategy;

        public TestData currentTool;
        public override void Awake()
        {
            base.Awake();            
            state = new PlayerStates(this);
            //테스트용
            interactType = InteractType.Draw;

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
        public void SelectTool(ToolData tool)
        {
            if (tool.toolName == null)
                return;

            interactType = tool.interactType;
            switch(tool.interactType)
            {
                case InteractType.Talk:
                    SetInteractionStrategy(new TalkInteraction());
                    break;
                case InteractType.Gather:
                    SetInteractionStrategy(new GatherInteraction());
                    break;
                case InteractType.Draw:
                    SetInteractionStrategy(new DrawInteraction());
                    break;
            }
            Debug.Log(tool.toolName);
        }
        public void Check()
        {
            
        }
    }

}

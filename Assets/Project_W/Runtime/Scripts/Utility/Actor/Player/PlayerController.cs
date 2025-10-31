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
        private InputSystem inputSystem;
        public override void Awake()
        {
            inputSystem = InputSystem.Instance;
            inputSystem.Initialize(this);
            
            state = new PlayerStates(this);
            //테스트용
            interactType = InteractType.Draw;

            currentStrategy = new DrawInteraction();

            foreach(var state in state.GetAll())
                stateMachine.AddState(state);

            base.Awake();
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
            currentSpeed = isWalk ? walkSpeed : isRun ? runSpeed : walkSpeed;
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

        public void Check()
        {
            
        }
    }

}

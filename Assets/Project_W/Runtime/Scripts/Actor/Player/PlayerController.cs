namespace S
{
    using UnityEngine;
    using W;

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
        public bool lockDirection = false;

        public IInteractable currentTarget;
        public IInteractionStrategy currentStrategy;
        public ToolType toolType;

        public PotionThrowIndicator potionIndicator;

        public GameObject throwPotionPrefab;
        public GameObject throwPotion;
        public bool throwAniEnd = false;

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
            if (!lockDirection)
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
        public override void OnAnimationEnd(AnimationTag animationTag)
        {
            switch (animationTag)
            {
                case AnimationTag.Throw:
                    throwAniEnd = true;
                    animator.SetBool("isThrow", false);
                    break;
                case AnimationTag.Drink:
                    break;
            }
        }
        public void UpdateAim()
        {
            potionIndicator.UpdateAimIndicator(transform.position, dir);
        }
        public void SelectPotion()
        {
            //포션 선택하면 정보 넘기기
            if (throwPotion == null)
                throwPotion = Instantiate(throwPotionPrefab, transform.position, Quaternion.identity, transform);
            else
                throwPotion.SetActive(true);

            GetPotionStartPos();
        }
        public void GetPotionStartPos()
        {
            var startPos = (Vector2)transform.position + dir.normalized * potionIndicator.startOffset;
            throwPotion.transform.position = startPos;
        }
    }

}

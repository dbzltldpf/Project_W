namespace S
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.U2D.Animation;
    using W;

    public enum InteractType
    {
        Talk,
        Gather,
        Draw
    }
    public class PlayerController : Actor
    {
        public Vector2 MoveInput { get; private set; }
        public PlayerStates State { get; private set; }
        public bool IsRun { get; private set; }
        public bool IsWalk { get; private set; }

        public bool CanInterect { get; private set; }
        public bool LockDirection { get; private set; }

        public IInteractable currentTarget;
        public IInteractionStrategy currentStrategy;
        public ToolType toolType;

        public PotionThrowIndicator potionIndicator;

        //포션 관련
        public GameObject throwPotionPrefab;
        public GameObject drinkPotionPrefab;
        public GameObject throwPotion;
        public GameObject drinkPotion;
        public List<SpriteLibraryAsset> drinkSpriteLibrary;
        public bool throwAniEnd = false;

        public override void Awake()
        {
            base.Awake();            
            State = new PlayerStates(this);
            currentStrategy = new DrawInteraction();

            foreach(var state in State.GetAll())
                stateMachine.AddState(state);

            IsRun = false;
            IsWalk = false;
            CanInterect = false;
        }
        public override void Start()
        {
            base.Start();
            ChangeState(State.Idle);
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
            this.MoveInput = moveInput;
            isMoving = moveInput != Vector2.zero;
            animator.SetBool("isMove", isMoving);
            SetRun(isRun);
            SetWalk(isWalk);
            currentSpeed = isWalk ? walkSpeed : isRun ? runSpeed : moveSpeed;
            if (!LockDirection)
                ChangeState(isMoving ? State.Move : State.Idle);
        }
        public void SetLockDirection(bool value)
        {
            LockDirection = value;
        }
        public void SetCanInterect(bool value)
        {
            CanInterect = value;
        }
        public void SetRun(bool value)
        {
            if (IsRun == value) return;
            IsRun = value;
        }
        public void SetWalk(bool value)
        {
            if (IsWalk == value) return;
            IsWalk = value;
            animator.SetBool("isWalk", value);
        }

        public void TryInterect()
        {
            if (!CanInterect || currentTarget == null)
                return;

            currentStrategy?.Interact(this, currentTarget);
        }
        public void SetInteractionStrategy(IInteractionStrategy strategy)
        {
            currentStrategy = strategy;
        }
        public void SelectPotion(PotionType potionType)
        {
            switch (potionType)
            {
                case PotionType.Throw:
                    //던지는 물약 ui변경
                    if (throwPotion == null)
                        throwPotion = Instantiate(throwPotionPrefab, transform.position, Quaternion.identity, transform);
                    else
                    {
                        throwPotion.SetActive(true);
                    }
                    GetThrowPotionStartPos();
                    break;
                case PotionType.Drink:
                    //마시는 물약 spritelibrary 변경
                    if (drinkPotion == null)
                        drinkPotion = Instantiate(drinkPotionPrefab, transform.position, Quaternion.identity, transform);
                    else
                        drinkPotion.SetActive(true);
                    GetDrinkPotionStartPos();
                    break;
            }

            //포션 선택하면 정보 넘기기
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
                    animator.SetBool("isDrink", false);
                    ChangeState(State.Idle);
                    InputSystem.Instance.ChangeState(new DefaultInputSystem());
                    drinkPotion.gameObject.SetActive(false);
                    break;
            }
        }
        public void UpdateAim()
        {
            potionIndicator.UpdateAimIndicator(transform.position, dir);
        }
        public void GetDrinkPotionStartPos()
        {
            var startPos = (Vector2)transform.position + new Vector2(-1, 0) * 0.1f;
            drinkPotion.transform.position = startPos;
        }

        public void GetThrowPotionStartPos()
        {
            var startPos = (Vector2)transform.position + dir.normalized * potionIndicator.startOffset;
            throwPotion.transform.position = startPos;
        }
    }

}

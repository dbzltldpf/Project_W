namespace S
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface IInteractable
    {
        void Interact();
        void ShowInteractUI();
        void HideInteractUI();
        Vector2 GetPosition();
        NonPlayerType GetNonPlayerType();
        bool CanInteractType(InteractType type);
    }
    public enum NonPlayerType
    {
        Npc,
        Creature
    }
    public class NonPlayerController : Actor, IInteractable
    {
        public NonPlayerType nonPlayerType;
        public bool isMovable;

        public NonPlayerStates state;
        float radius = 1f;
        public Vector2 targetPos;
        public Vector2 anchorPos;

        public List<InteractType> availableInteractions = new List<InteractType>();
        public override void Awake()
        {
            base.Awake();
            state = new NonPlayerStates(this);
            isMovable = true;
            anchorPos = transform.position;

            foreach (var state in state.GetAll())
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
            ChangeState(isMoving ? state.Move : state.Idle);
        }
        public Vector2 GetPosition()
        {
            return transform.position;
        }
        public bool CanInteractType(InteractType type)
        {
            return availableInteractions.Contains(type);
        }
        public void ShowInteractUI()
        {
            //상호작용 종류 별 마크 필요
            transform.GetChild(1).gameObject.SetActive(true);
        }
        public void HideInteractUI()
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }

        public void Interact()
        {
            Debug.Log($"{transform.name}");
        }
        public NonPlayerType GetNonPlayerType()
        {
            return nonPlayerType;
        }
        public void SetRandomTarget()
        {
            //NPC는 타일맵으로 이동, Creature는 포지션으로 이동
            if (nonPlayerType == NonPlayerType.Npc)
            {
                //targetPos = pos + Random.insideUnitCircle * radius;
            }
            else
            {
                //Random.insideUnitCircle(0.0중심으로 반지름 안에 랜덤한 점 반환)
                targetPos = anchorPos + Random.insideUnitCircle * radius;
            }
        }

    }
}

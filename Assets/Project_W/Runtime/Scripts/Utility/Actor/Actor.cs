namespace S
{
    using UnityEngine;
    public abstract class Actor : MonoBehaviour
    {
        protected CharacterStateMachine stateMachine;
        [SerializeField]
        public Vector2 dir;
        public Rigidbody2D rb { get; private set; }
        public Animator animator { get; private set; }

        
        public readonly float moveSpeed = 1f;

        public readonly float runSpeed = 2f;

        public readonly float walkSpeed = 0.5f;

        public float currentSpeed;
        public bool isMoving;

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            stateMachine = new CharacterStateMachine();
            currentSpeed = moveSpeed;
        }
        public virtual void Start()
        {
            isMoving = false;
        }
        public virtual void Update()
        {
            stateMachine.Update();
        }
        public virtual void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        public void ChangeState(ICharacterState newState)
        {
            stateMachine.ChangeState(newState.GetType());
        }
    }
}

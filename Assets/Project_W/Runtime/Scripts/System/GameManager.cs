namespace S
{
    using UnityEngine;
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController player;
        public void Start()
        {
            Debug.Log("GameManager Awake");
            InputSystem.Instance.Initialize(player);
        }
    }
}

using UnityEngine;


namespace Game
{
    [RequireComponent (typeof(InputHandler))]
    [RequireComponent (typeof(PlayerAction))]
    [RequireComponent (typeof(PlayerMovement))]
    [RequireComponent (typeof(AnimatorHandler))]
    public class Player : MonoBehaviour
    {
        public static Player PlayerInstance;

        [SerializeField] private PlayerState playerState;
        private InputHandler inputHandler;
        private PlayerAction playerAction;
        private PlayerMovement playerMovement;
        private AnimatorHandler animatorHandler;

        private void Awake()
        {
            PlayerInstance = PlayerInstance != null ? PlayerInstance : this;
        }

        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerAction = GetComponent<PlayerAction>();
            playerMovement = GetComponent<PlayerMovement>();
            animatorHandler = GetComponent<AnimatorHandler>();
            animatorHandler.StartAction();
        }
    }
}

using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(PlayerAction))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(AnimatorHandler))]
    public class Player : MonoBehaviour
    {
        public static Player PlayerInstance;

        [SerializeField] public PlayerState playerState;
        public InputHandler inputHandler { get; private set; }
        private PlayerAction playerAction;
        private PlayerMovement playerMovement;
        public AnimatorHandler animatorHandler;


        private bool isFacingRight = true;


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

        private void Update()
        {
            inputHandler.UpdateInputValues();
            if (inputHandler.MovementInput.x != 0)
            {
                CheckFaceDirection(inputHandler.MovementInput.x > 0);
            }
            playerMovement.HandleMovement(inputHandler.MovementInput, Time.deltaTime);
            playerMovement.HandleJumping(inputHandler.JumpInput);
        }

        public void CheckFaceDirection(bool isMovingRight)
        {
            if (isMovingRight != isFacingRight)
                Turn();
        }

        private void Turn()
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = Player.PlayerInstance.transform.localScale;
            scale.x *= -1;
            Player.PlayerInstance.transform.localScale = scale;
        }

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;


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
            transform.position = playerState.respawnPoint;
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
            
            playerMovement.HandleJumping(inputHandler.JumpInput);
        }

        private void FixedUpdate()
        {
            if (inputHandler.MovementInput.x != 0)
            {
                CheckFaceDirection(inputHandler.MovementInput.x > 0);
            }
            playerMovement.HandleMovement(inputHandler.MovementInput);
        }

        public void CheckFaceDirection(bool isMovingRight)
        {
            if (isMovingRight != isFacingRight)
                Turn();
        }

        private void Turn()
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        public void SetSpawnpoint(Vector2 spawnpoint)
        {
            playerState.respawnPoint = spawnpoint;
        }

        private void Die()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

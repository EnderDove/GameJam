using UnityEngine;


namespace Game
{
    [RequireComponent (typeof (InputHandler), typeof(PlayerAction), typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerState playerState;
        private InputHandler inputHandler;
        private PlayerAction playerAction;
        private PlayerMovement playerMovement;

        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerAction = GetComponent<PlayerAction>();
            playerMovement = GetComponent<PlayerMovement>();
        }
    }
}

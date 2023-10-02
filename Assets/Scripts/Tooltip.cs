using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    [RequireComponent(typeof( Collider2D))]
    public class Tooltip : MonoBehaviour
    {
        public Canvas canvas;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            canvas.gameObject.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            canvas.gameObject.SetActive(false);
        }
    }
}

using Game;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Player.PlayerInstance.SetSpawnpoint(transform.position);
    }
}

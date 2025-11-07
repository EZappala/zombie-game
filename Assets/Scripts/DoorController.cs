using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class DoorController : MonoBehaviour
{
    private GameObject player;
    private Player player_comp;
    private GameManager game_manager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError($"No player in scene {SceneManager.GetActiveScene().name}");
        }

        player_comp = player.GetComponent<Player>();
        game_manager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player)
            return;

        if (!player_comp.HasKey)
            return;

        Debug.Log("Got to door!");

        game_manager.finish_level();
    }
}

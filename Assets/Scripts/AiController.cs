using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class AiController : MonoBehaviour
{
    private GameObject player;
    private GameManager manager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError($"No player in scene {SceneManager.GetActiveScene().name}");
        }

        manager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player)
            return;

        if (manager == null)
            manager = GameObject.FindFirstObjectByType<GameManager>();
        manager.finish_level(true);
    }
}

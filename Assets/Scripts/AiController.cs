using UnityEngine;

public sealed class AiController : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player)
            return;

        Debug.Log("GAME OVER");
    }
}

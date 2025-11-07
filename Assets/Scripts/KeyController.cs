using UnityEngine;

public class KeyController : MonoBehaviour
{
    private GameObject player;
    private Player player_comp;

    [SerializeField]
    private float rotation_speed = 50f;

    [SerializeField]
    private float bob_speed = 1f;

    [SerializeField]
    private float bob_amplitude = 0.1f;

    private Vector3 startPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_comp = player.GetComponent<Player>();
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * bob_speed) * bob_amplitude;
        transform.position = newPosition;

        transform.Rotate(Vector3.forward * rotation_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player)
            return;

        player_comp.HasKey = true;
    }
}

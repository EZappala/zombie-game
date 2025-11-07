using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public sealed class Player : Entity
{
    private bool has_key;

    public bool HasKey
    {
        get => has_key;
        set
        {
            has_key = value;
            Debug.Log("PLAYER HAS KEY!");
        }
    }
}

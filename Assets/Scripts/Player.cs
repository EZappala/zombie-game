using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.InputSystem;
using System.Linq;
#endif

[RequireComponent(typeof(PlayerController))]
public sealed class Player : Entity
{
    private bool has_key;

    // private InGameUiController ui_controller;

    public bool HasKey
    {
        get => has_key;
        set { has_key = value; }
    }

    void Start()
    {
#if UNITY_EDITOR
        var asset = InputSystem.actions;
        Debug.Log($"Asset enabled: {asset?.enabled}");
        if (asset != null)
        {
            Debug.Log(
                "ActionMaps: "
                    + string.Join(", ", asset.actionMaps.Select(m => m.name + ":" + m.enabled))
            );
        }
#endif
    }
}

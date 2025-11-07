using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private InputActionAsset ia;
    private InputActionMap ia_map;
    private InputAction attack;

    private void Awake()
    {
        ia = Instantiate(InputSystem.actions);
        ia_map = ia.actionMaps.Where(m => m.name == "Player").First();
        attack = ia_map.FindAction("Attack");
        if (attack == null)
        {
            Debug.LogError("No attack action found", this);
            return;
        }

        attack.performed += OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("L1");
    }

    private void OnEnable()
    {
        attack.Enable();
    }

    private void OnDisable()
    {
        attack.performed -= OnAttackPerformed;
        attack.Disable();
    }
}

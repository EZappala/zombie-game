using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private InputAction submit;

    private void Awake()
    {
        submit = InputSystem.actions.FindAction("Submit");
        if (submit == null)
        {
            Debug.LogError("No continue action found", this);
            return;
        }

        submit.performed += OnSubmitPerformed;
    }

    private void OnSubmitPerformed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("L1");
    }

    private void OnEnable()
    {
        submit.Enable();
    }

    private void OnDisable()
    {
        submit.performed -= OnSubmitPerformed;
        submit.Disable();
    }
}

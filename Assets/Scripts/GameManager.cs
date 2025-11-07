using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField]
    private int total_keys = 1;
    private int score;

    private bool goal_achieved;
    private Dictionary<string, bool> levels = new()
    {
        { "L1", false },
        { "L2", false },
        { "L3", false },
    };

    [SerializeField]
    private InGameUiController ui;

    private void Awake()
    {
        if (ui == null)
            ui = FindFirstObjectByType<InGameUiController>();
        if (ui == null)
        {
            Debug.LogError("No InGameUiController found in scene", this);
            return;
        }

        ui.update_score(score, total_keys);
        DontDestroyOnLoad(gameObject);
    }

    internal void update_score()
    {
        score += 1;
        ui.update_score(score, total_keys);
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        var cont = FindFirstObjectByType<PlayerController>().ia_map.FindAction("Attack");
        cont.performed -= OnAttackPerformed;
        cont.Disable();
    }

    internal void got_key()
    {
        score += 1;
        ui.update_score(score, total_keys);
    }

    internal void finish_level(bool killed = false)
    {
        if (ui == null)
            ui = FindFirstObjectByType<InGameUiController>();
        if (killed)
        {
            ui.set_game_over("You Lose!\n(CLICK to continue)");
            InputSystem.DisableAllEnabledActions();
            var cont = FindFirstObjectByType<PlayerController>().ia_map.FindAction("Attack");
            cont.Enable();
            cont.performed += OnAttackPerformed;
            return;
        }

        string scene_name = SceneManager.GetActiveScene().name;
        if (string.IsNullOrEmpty(scene_name))
        {
            Debug.LogError($"Invalid scene name: {scene_name}");
        }

        levels[scene_name] = true;
        score = 0;
        if (levels.All(l => l.Value))
        {
            ui.set_game_over("You Win!\n(CLICK to continue)");
            goal_achieved = true;
            InputSystem.DisableAllEnabledActions();
            var cont = FindFirstObjectByType<PlayerController>().ia_map.FindAction("Attack");
            cont.Enable();
            cont.performed += OnAttackPerformed;
            return;
        }

        string next_scene_name = levels.Where(l => !l.Value).First().Key;
        if (string.IsNullOrEmpty(next_scene_name))
        {
            Debug.LogError($"Invalid scene name: {next_scene_name}");
        }
        SceneManager.LoadScene(next_scene_name);
    }
}

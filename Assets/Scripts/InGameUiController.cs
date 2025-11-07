using UnityEngine;
using UnityEngine.UIElements;

public class InGameUiController : MonoBehaviour
{
    private UIDocument doc;
    private VisualElement root;
    private TextElement score;
    private TextElement timer;
    private VisualElement final;
    private TextElement final_text;

    [SerializeField]
    private GameManager game_manager;

    private void Awake()
    {
        if (game_manager == null)
            game_manager = FindFirstObjectByType<GameManager>();
        if (doc == null && !TryGetComponent(out doc))
        {
            Debug.LogError("No UIDocument component", this);
            return;
        }

        root = doc.rootVisualElement;
        score = root.Q<TextElement>("Score");
        timer = root.Q<TextElement>("Timer");
        final = root.Q<VisualElement>("Final");
        final_text = root.Q<TextElement>("FinalText");

        final.style.visibility = Visibility.Hidden;
    }

    internal void update_score(int new_score, int max_score = 1)
    {
        score.text = $"{new_score}/{max_score}";
    }

    internal void set_game_over(string text)
    {
        final.style.visibility = Visibility.Visible;
        final_text.text = text;
    }
}

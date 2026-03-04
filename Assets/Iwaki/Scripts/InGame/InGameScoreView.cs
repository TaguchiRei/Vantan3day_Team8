using TMPro;
using UnityEngine;

public class InGameScoreView : MonoBehaviour
{
    [SerializeField] private InGameManager _inGameManager;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        UpdateView(0);
        _inGameManager.OnScoreChanged += UpdateView;
    }

    private void UpdateView(int score)
    {
        _text.text = score.ToString();
    }
}

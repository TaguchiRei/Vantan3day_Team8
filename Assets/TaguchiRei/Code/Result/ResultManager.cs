using TMPro;
using UnityEngine;
using unityroom.Api;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Start()
    {
        var result = GameManager.Instance.GetResult();
        var score = GameManager.Instance.ResultScore;
        _spriteRenderer.sprite = result.Sprite;
        _tmp.text = result.EndingText;
        _scoreText.text = "Score : " + score;

        UnityroomApiClient.Instance.SendScore(1, 123.45f, ScoreboardWriteMode.Always);
    }
}
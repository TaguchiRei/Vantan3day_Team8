using TMPro;
using UnityEngine;
using unityroom.Api;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Start()
    {
        var result = GameManager.Instance.GetResult();
        var score = GameManager.Instance.ResultScore;
        EndingTextData text;
        if (result.EndingTexts.Length > 1)
        {
            text = result.EndingTexts.First(e => e.Type == GameManager.Instance.LastStamp);
        }
        else
        {
            text = result.EndingTexts[0];
        }

        _spriteRenderer.sprite = result.Sprite;
        _tmp.text = text.Text;
        _scoreText.text = "Score : " + score;

        UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.Always);
    }
}
using System;
using TMPro;
using UnityEngine;
using unityroom.Api;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private float _scoreShowTime = 2;
    [SerializeField] private AnimationCurve _curve;

    private int _showNumber;
    private int _score;

    private float _totalTime;

    public void Start()
    {
        var result = GameManager.Instance.GetResult();
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
        _scoreText.text = "Score : 0";

        _score = GameManager.Instance.ResultScore;

        UnityroomApiClient.Instance.SendScore(1, _score, ScoreboardWriteMode.Always);
    }

    public void Update()
    {
        if (_totalTime < _scoreShowTime)
        {
            _totalTime += Time.deltaTime;
            float t = Mathf.Clamp01(_totalTime / _scoreShowTime);
            float rate = _curve.Evaluate(t);
            float value = Mathf.LerpUnclamped(0f, _score, rate);

            _scoreText.text = $"Score : {(int)value}";
        }
    }
}
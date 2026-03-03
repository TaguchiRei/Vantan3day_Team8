using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Start()
    {
        var result = GameManager.Instance.GetResult();
        _spriteRenderer.sprite = result.Sprite;
        _tmp.text = result.EndingText;
        _scoreText.text = "Score : " + GameManager.Instance.ResultScore;
    }
}
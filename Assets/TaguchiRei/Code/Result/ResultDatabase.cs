using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResultDatabase : MonoBehaviour
{
    public static ResultDatabase Instance { get; private set; }

    [SerializeField] private int _saveCount = 10;
    [SerializeField] private TMP_Text _rankingText;
    [SerializeField] private TMP_Text _rankingText2;
    [SerializeField] private TMP_Text _returnButtonText;
    [SerializeField] private GameObject _stamp;

    private bool _isReturnning;


    private List<int> _scores = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);
    }

    public void ShowRanking()
    {
        gameObject.SetActive(true);
        _stamp.SetActive(false);
        _returnButtonText.color = Color.white;
    }

    public void HideRanking()
    {
        if (_isReturnning) return;
        SoundManager.PlaySE(SEType.HankoPress);
        _returnButtonText.color = Color.black;
        Invoke(nameof(SetDisable), 0.5f);
        _isReturnning = true;
    }

    public void SaveScore(int score)
    {
        _scores.Add(score);

        _scores.Sort();
        _scores.Reverse();

        if (_scores.Count > _saveCount)
        {
            _scores.RemoveAt(_scores.Count - 1);
        }

        UpdateScore();
    }

    private void SetDisable()
    {
        _isReturnning = false;
        gameObject.SetActive(false);
    }

    private void UpdateScore()
    {
        _scores.Sort();
        _scores.Reverse();

        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();

        //１行開ける
        sb1.AppendLine("スコアランキング\n");
        sb2.AppendLine("\n");

        for (int i = 0; i < _scores.Count; i++)
        {
            if (i < _saveCount / 2)
            {
                sb1.AppendLine($"{i + 1}位 : {_scores[i]}");
            }
            else
            {
                sb2.AppendLine($"{i + 1}位 : {_scores[i]}");
            }
        }

        _rankingText.text = sb1.ToString();
        _rankingText2.text = sb2.ToString();
    }
}
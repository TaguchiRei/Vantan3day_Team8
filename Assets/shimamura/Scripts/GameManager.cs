using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EndingDatabase _endingDatabase;
    public static GameManager Instance;

    public int ResultScore;
    public EndingType EndingType;

    private StampType _lastStamp;

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
        }
    }

    private void Start()
    {
        SoundManager.PlayBGM(BGMType.Title);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
        SoundManager.PlayBGM(BGMType.Title);
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene("InGame");
        SoundManager.PlayBGM(BGMType.InGame);
    }

    public void LoadResultScene()
    {
        var bgmType = EndingType switch
        {
            EndingType.Good => BGMType.ResultGood,
            EndingType.Normal => BGMType.ResultNormal,
            _ => BGMType.ResultBad
        };
        SoundManager.PlayBGM(bgmType);
        SceneManager.LoadScene("Result");
    }

    public void SaveResult(int score, EndingType endingType, StampType lastStamp)
    {
        ResultScore = score;
        EndingType = endingType;
        _lastStamp = lastStamp;
    }

    public EndingEntry GetResult()
    {
        return _endingDatabase.GetEnding(EndingType);
    }
}
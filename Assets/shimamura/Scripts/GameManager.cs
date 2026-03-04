using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EndingDatabase _endingDatabase;
    [SerializeField] private FadeManager _fadeManager;
    public static GameManager Instance;

    public int ResultScore;
    public EndingType EndingType;

    public StampType LastStamp;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        _ = _fadeManager.FadeOut();
        SoundManager.PlayBGM(BGMType.Title);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _ = _fadeManager.FadeOut();
    }

    public void LoadTitleScene()
    {
        _ = _fadeManager.FadeIn(() =>
        {
            SoundManager.PlayBGM(BGMType.Title);
            SceneManager.LoadScene("Title");
        });
    }

    public void LoadInGameScene()
    {
        _ = _fadeManager.FadeIn(() =>
        {
            SoundManager.PlayBGM(BGMType.InGame);
            SceneManager.LoadScene("InGame");
        });
    }

    public void LoadResultScene()
    {
        _ = _fadeManager.FadeIn(() =>
        {
            SceneManager.LoadScene("Result");
            var bgmType = EndingType switch
            {
                EndingType.Good => BGMType.ResultGood,
                EndingType.Normal => BGMType.ResultNormal,
                _ => BGMType.ResultBad
            };
            SoundManager.PlayBGM(bgmType);
        });
    }

    public void SaveResult(int score, EndingType endingType, StampType lastStamp)
    {
        ResultScore = score;
        EndingType = endingType;
        LastStamp = lastStamp;
    }

    public EndingEntry GetResult()
    {
        return _endingDatabase.GetEnding(EndingType);
    }
}
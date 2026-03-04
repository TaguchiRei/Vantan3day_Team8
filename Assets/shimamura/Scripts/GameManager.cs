using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EndingDatabase _endingDatabase;

    [SerializeField]
    private FadeManager _fadeManager;

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
    }

    private void Start()
    {
        _ = _fadeManager.FadeOut();
    }

    public async void LoadTitleScene()
    {
        await _fadeManager.FadeIn(destroyCancellationToken);
        await SceneManager.LoadSceneAsync("Title");
        SoundManager.PlayBGM(BGMType.Title);
        await _fadeManager.FadeOut(destroyCancellationToken);
    }

    public async void LoadInGameScene()
    {
        await _fadeManager.FadeIn(destroyCancellationToken);
        await SceneManager.LoadSceneAsync("InGame");
        SoundManager.PlayBGM(BGMType.InGame);
        await _fadeManager.FadeOut(destroyCancellationToken);
    }

    public async void LoadResultScene()
    {
        await _fadeManager.FadeIn(destroyCancellationToken);
        await SceneManager.LoadSceneAsync("Result");
        var bgmType = EndingType switch
        {
            EndingType.Good => BGMType.ResultGood,
            EndingType.Normal => BGMType.ResultNormal,
            _ => BGMType.ResultBad
        };
        SoundManager.PlayBGM(bgmType);
        await _fadeManager.FadeOut(destroyCancellationToken);
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
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private string _inGame = "SampleScene";

    //private EndingType _currentEnding = EndingType.None;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //public void SetEnding(EndingType endingType)
    //{
    //    _currentEnding = endingType;
    //}

    public void StartScene()
    {
        SoundManager.PlayBGM(BGMType.Title);
    }

    private void OnSceneLoad(Scene scene)
    {
        switch (scene.name)
        {
            case "Titel":
                SoundManager.PlayBGM(BGMType.Title);
                break;
            case "InGame":
                SoundManager.PlayBGM(BGMType.InGame);
                break;
            case "Result":
                SoundManager.PlayBGM(BGMType.Result);
                break;

        }



    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public void LoadScene(BGMType bgmType)
    {
        SceneManager.LoadScene(bgmType.ToString());
        SoundManager.PlayBGM(bgmType);
    }

    private void OnSceneLoad(Scene scene)
    {
        if (Enum.TryParse(scene.name, out BGMType bgmType))
        {
            SoundManager.PlayBGM(bgmType);
        }
    }
}

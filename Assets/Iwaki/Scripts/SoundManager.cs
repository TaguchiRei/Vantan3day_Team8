using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private BGMDatabase _bgmDatabase;
    [SerializeField] private SEDatabase _seDatabase;

    [SerializeField] private AudioSource _bgmAudioSource;
    [SerializeField] private AudioSource _seAudioSource;

    private float _defaultSEVolume;

    private static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundManager が見つかりません");
            }

            return _instance;
        }
    }

    private static SoundManager _instance;

    private void Awake()
    {
        // 既に存在したら破棄する
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        _instance = this;
        _defaultSEVolume = _seAudioSource.volume;
    }

    public static void PlayBGM(BGMType type)
    {
        Instance.Play(type);
    }

    public static void PlaySE(SEType type)
    {
        Instance.Play(type, Instance._defaultSEVolume);
    }

    public static void PlaySE(SEType type, float volume)
    {
        Instance.Play(type, volume);
    }

    private void Play(BGMType type)
    {
        if (_bgmDatabase == null)
        {
            Debug.LogError("[SoundManager] BGM Databaseが存在しません", this);
            return;
        }

        var bgm = _bgmDatabase.GetBGM(type);
        _bgmAudioSource.clip = bgm;
        _bgmAudioSource.Play();
    }

    private void Play(SEType type, float volume)
    {
        if (_seDatabase == null)
        {
            Debug.LogError("[SoundManager] SE Databaseが存在しません", this);
            return;
        }

        var se = _seDatabase.GetSE(type);
        _seAudioSource.PlayOneShot(se, volume);
    }
}
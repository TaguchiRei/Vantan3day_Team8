using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : MonoBehaviour
{
    public float NowTime;
    [SerializeField] private int _timeLimit;
    [SerializeField] private Document _documentPrefab;
    [SerializeField] private DocumentDataBase _documentDB;
    [SerializeField] private DocumentTextDatabase _documentTextDB;
    [SerializeField] private StampInstance _stampInstance;
    [SerializeField] private StampDataBase _stampDB;

    [SerializeField] private Bundle _startObject;

    [Header("GameSetting")] [SerializeField, Min(0)]
    private int _score = 100;

    [SerializeField, Min(0)] private int _bonus = 50;
    [SerializeField, Min(0)] private float _bonusTime = 5;
    [SerializeField, Min(0)] private int _missScore = 50;
    [SerializeField, Min(0)] private int _badLimit = 5;

    [Header("StartSetting")] [SerializeField]
    private float _startTime = 1.5f;


    private Document _document;
    private DocumentData _documentData;

    private Coroutine _routine;

    private int _totalScore = 0;
    private float _timeCount = 0;
    private bool _miss;
    private float _gameStartTime;

    public event Action<int> OnScoreChanged;
    public int TimeLimit => _timeLimit;

    private void Start()
    {
        NowTime = TimeLimit;
        _startObject.PlayStartAction += PlayStart;
        _startObject.Animator.SetTrigger("Start");
        _routine = StartCoroutine(InGameTimer());
    }

    private void Update()
    {
        _timeCount += Time.deltaTime;
    }

    private void OnDestroy()
    {
        StopCoroutine(_routine);
    }

    private void PlayStart()
    {
        InputRegistration(true);
        GenerateDocument();
        InputDispatcher.Interface.SwitchActionMap(nameof(ActionMaps.InGame));
        InputDispatcher.Interface.EnableInput();
    }


    private void InputRegistration(bool registration)
    {
        var reg = registration ? Registration.Register : Registration.UnRegister;

        InputDispatcher.Interface.ChangeActionRegistrationStart(
            nameof(ActionMaps.InGame),
            nameof(InGameActions.PersonalSeal),
            OnPersonalSeal, reg);
        InputDispatcher.Interface.ChangeActionRegistrationStart(
            nameof(ActionMaps.InGame),
            nameof(InGameActions.CompanySeal),
            OnCompanySeal, reg);

        InputDispatcher.Interface.ChangeActionRegistrationStart(
            nameof(ActionMaps.InGame),
            nameof(InGameActions.Pass),
            OnPath, reg);
    }

    private void OnPersonalSeal(InputAction.CallbackContext context)
    {
        PressDocument(StampType.PersonalSeal);
    }

    private void OnCompanySeal(InputAction.CallbackContext context)
    {
        PressDocument(StampType.CompanySeal);
    }

    private void OnPath(InputAction.CallbackContext context)
    {
        PathDocument();
    }

    /// <summary>
    /// 完全にランダムな物を生成するように。
    /// </summary>
    private void GenerateDocument()
    {
        _documentData = _documentDB.GetRandomDocument();
        _document = Instantiate(_documentData.Prefab).GetComponent<Document>();

        var documentText = _documentData.DocumentType switch
        {
            DocumentType.Proposal => _documentTextDB.GetRandomProposalDocument().GetText(),
            DocumentType.Resume => _documentTextDB.GetRandomResumeDocument().GetText(),
            _ => Array.Empty<string>()
        };
        _document.SetText(documentText);
    }

    /// <summary>
    /// スタンプを押すときに呼び出す。
    /// 成功判定なども行っているので、演出を足したいときはここで
    /// </summary>
    /// <param name="stampType"></param>
    private void PressDocument(StampType stampType)
    {
        if (_document == null) return;
        var stamp = _stampDB.AllStamp.Find(s => s.Type == stampType);
        _stampInstance.PressTheStamp(stamp.MainSprite);
        SoundManager.PlaySE(SEType.HankoPress);
        if (_documentData.CorrectStamp == stampType || _documentData.CorrectStamp == StampType.Both)
        {
            switch (_documentData.EndingFlag)
            {
                case EndingFlag.Marriage:
                    InputRegistration(false);
                    InputDispatcher.Interface.DisableInput();
                    GameManager.Instance.SaveResult(_totalScore, EndingType.marriage, stampType);
                    GameManager.Instance.LoadResultScene();
                    break;
                case EndingFlag.Divorce:
                    InputRegistration(false);
                    InputDispatcher.Interface.DisableInput();
                    GameManager.Instance.SaveResult(_totalScore, EndingType.divorce, stampType);
                    GameManager.Instance.LoadResultScene();
                    break;
                case EndingFlag.DevilSummon:
                    InputRegistration(false);
                    InputDispatcher.Interface.DisableInput();
                    GameManager.Instance.SaveResult(_totalScore, EndingType.devil, stampType);
                    GameManager.Instance.LoadResultScene();
                    break;
                default:
                    SoundManager.PlaySE(SEType.DocumentCorrect);
                    _totalScore += _score;
                    if (_timeCount < _bonusTime)
                    {
                        _totalScore += _bonus;
                    }

                    OnScoreChanged?.Invoke(_totalScore);
                    _timeCount = 0;
                    break;
            }

            _document.HideDoc(stamp.ShadowSprite, true);
        }
        else
        {
            SoundManager.PlaySE(SEType.DocumentMistake);

            _totalScore -= _missScore;
            _document.HideDoc(stamp.ShadowSprite, true);
        }

        GenerateDocument();
    }

    /// <summary>
    /// 書類をパスしたときに呼び出す。
    /// </summary>
    private void PathDocument()
    {
        SoundManager.PlaySE(SEType.DocumentDispose);

        _document.HideDoc(null, false);
        GenerateDocument();
    }

    /// <summary>
    /// 時間切れ終了時、スコア毎に異なるエンディングを出させるためのクラス
    /// </summary>
    /// <returns></returns>
    private IEnumerator InGameTimer()
    {
        _gameStartTime = Time.time;
        for (int i = 0; i < _timeLimit; i++)
        {
            yield return new WaitForSeconds(1);
            NowTime = Time.time - _gameStartTime;
        }

        EndingType type;
        if (_totalScore < _score + _badLimit)
        {
            type = EndingType.Bad;
        }
        else if (_miss)
        {
            type = EndingType.Normal;
        }
        else
        {
            type = EndingType.Good;
        }

        InputDispatcher.Interface.DisableInput();
        InputRegistration(false);
        GameManager.Instance.SaveResult(_totalScore, type, default);
        GameManager.Instance.LoadResultScene();
    }
}
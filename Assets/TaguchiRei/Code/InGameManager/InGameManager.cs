using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private int _timeLimit;
    [SerializeField] private Document _documentPrefab;
    [SerializeField] private DocumentDataBase _documentDB;
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
    
    public event Action<int> OnScoreChanged;
    public int TimeLimit => _timeLimit;

    private void Start()
    {
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
        InputDispatcher.Interface.DisableInput();
        InputRegistration(false);
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
        _document = Instantiate(_documentPrefab);
        _documentData = _documentDB.Document[Random.Range(0, _documentDB.Document.Count)];
        _document.ShowDoc(_documentData.Image);
    }

    /// <summary>
    /// スタンプを押すときに呼び出す。
    /// 成功判定なども行っているので、演出を足したいときはここで
    /// </summary>
    /// <param name="stampType"></param>
    private void PressDocument(StampType stampType)
    {
        var stamp = _stampDB.AllStamp.Find(s => s.Type == stampType);
        _stampInstance.PressTheStamp(stamp.MainSprite);
        if (_documentData.CorrectStamp == stampType || _documentData.CorrectStamp == StampType.Both)
        {
            SoundManager.PlaySE(SEType.HankoPress);

            switch (_documentData.EndingFlag)
            {
                case EndingFlag.Marriage:
                    InputRegistration(false);
                    InputDispatcher.Interface.DisableInput();
                    GameManager.Instance.SaveResult(_totalScore, EndingType.marriage, stampType);
                    GameManager.Instance.LoadResultScene();
                    return;
                case EndingFlag.Divorce:
                    InputRegistration(false);
                    InputDispatcher.Interface.DisableInput();
                    GameManager.Instance.SaveResult(_totalScore, EndingType.divorce, stampType);
                    GameManager.Instance.LoadResultScene();
                    return;
                case EndingFlag.DevilSummon:
                    InputRegistration(false);
                    InputDispatcher.Interface.DisableInput();
                    GameManager.Instance.SaveResult(_totalScore, EndingType.devil, stampType);
                    GameManager.Instance.LoadResultScene();
                    return;
                default:
                    _document.HideDoc(stamp.ShadowSprite, true);
                    _totalScore += _score;
                    if (_timeCount < _bonusTime)
                    {
                        _totalScore += _bonus;
                    }
                    OnScoreChanged?.Invoke(_totalScore);

                    break;
            }

            _timeCount = 0;
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
        yield return new WaitForSeconds(_timeLimit);

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
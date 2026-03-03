using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private int _timeLimit;
    [SerializeField] private Document _documentPrefab;
    [SerializeField] private DocumentDataBase _documentDB;
    [SerializeField] private StampInstance _stampInstance;
    [SerializeField] private StampDataBase _stampDB;

    private Document _document;
    private DocumentData _documentData;

    private int _score = 0;

    private void Start()
    {
    }

    private void GenerateDocument()
    {
        _document = Instantiate(_documentPrefab);
        _documentData = _documentDB.Document[0];
        _document.ShowDoc(_documentData.Image);
    }

    private void PressTheStamp(StampType stampType)
    {
        var stamp = _stampDB.AllStamp.Find(s => s.Type == stampType);
        _stampInstance.PressTheStamp(stamp.Texture);
        if (_documentData.CorrectStamp == stampType)
        {
            _score++;

            switch (_documentData.EndingFlag)
            {
                case EndingFlag.Marriage:
                    GameManager.Instance.SaveResult(_score, EndingType.marriage);
                    break;
                case EndingFlag.Divorce:
                    GameManager.Instance.SaveResult(_score, EndingType.divorce);
                    break;
                case EndingFlag.DevilSummon:
                    GameManager.Instance.SaveResult(_score, EndingType.devil);
                    break;
                default:
                    break;
            }
        }
    }

    private async UniTask BeginCountDown()
    {
        await UniTask.Delay(3);
    }
}
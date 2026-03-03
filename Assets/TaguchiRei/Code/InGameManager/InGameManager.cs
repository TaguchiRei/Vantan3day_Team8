using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
	[SerializeField] private int _timeLimit;
	[SerializeField] private Document _documentPrefab;
	[SerializeField] private DocumentDataBase _documentDB;
	[SerializeField] private StampInstance _stampInstance;

	private Document document;

	private void Start()
	{

	}

	private void GenerateDocument()
	{
		document = Instantiate(_documentPrefab);
		var data = _documentDB.Document[0];
		document.ShowDoc(data.Image);
	}

	private void PressTheStamp(HankoType stampType)
	{
		_stampInstance.PressTheStamp();
	}

	private async UniTask BeginCountDown()
	{
		await UniTask.Delay(3);
	}
}

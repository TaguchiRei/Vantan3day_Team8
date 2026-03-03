using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
	[SerializeField] private int timeLimit;
	[SerializeField] private Document documentPrefab;
	[SerializeField] private DocumentDataBase documentDB;
	[SerializeField] private HankoInstance hankoInstance;

	private Document document;

	private void Start()
	{

	}

	private void GenerateDocument()
	{
		document = Instantiate(documentPrefab);
		var data = documentDB.Document[0];
		document.ShowDoc(data.Image);
	}

	private async UniTask BeginCountDown()
	{
		await UniTask.Delay(3);
	}
}

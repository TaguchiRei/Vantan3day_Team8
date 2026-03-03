using Cysharp.Threading.Tasks;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
	[SerializeField] private int timeLimit;
	[SerializeField] private Document documentPrefab;
	[SerializeField] private DocumentDataBase documentDB;
	private void Start()
	{

	}

	private void GenerateDocument()
	{
		var documentInstance = Instantiate(documentPrefab);
		var data = documentDB.Document[0];
		documentInstance.ShowDoc(data.Image);
	}

	private async UniTask BeginCountDown()
	{
		await UniTask.Delay(3);
	}
}

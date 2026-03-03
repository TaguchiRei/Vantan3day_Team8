using UnityEngine;

public class ButtonSupport : MonoBehaviour
{
    [SerializeField] private InListSceneName _sceneName;

    public void LoadScene()
    {
        switch (_sceneName)
        {
            case InListSceneName.Title:
                GameManager.Instance.LoadTitleScene();
                break;
            case InListSceneName.InGame:
                GameManager.Instance.LoadInGameScene();
                break;
            case InListSceneName.Result:
                GameManager.Instance.LoadResultScene();
                break;
        }
    }
}
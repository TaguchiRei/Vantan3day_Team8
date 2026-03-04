using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameStartDelay : MonoBehaviour
{
    [SerializeField] private float Delay;
    [SerializeField] private Animator animator;
    [SerializeField] private Image _hankoImage;
    [SerializeField] private Image _stampImage;
    [SerializeField] private StampDataBase _stampDB;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _stampImage.sprite = _stampDB.AllStamp[Random.Range(0, _stampDB.AllStamp.Count)].ShadowSprite;
    }

    public void OnClick()
    {
        _startButton.interactable = false;
        animator.Play("UI Hanko Animation");
        StartCoroutine(StartDelay());
    }
    
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Delay);
        GameManager.Instance.LoadInGameScene();
    }
}

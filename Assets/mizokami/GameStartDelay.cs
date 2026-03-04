using System.Collections;
using UnityEngine;

public class GameStartDelay : MonoBehaviour
{
    [SerializeField] private float Delay;
    [SerializeField] Animator animator;
    [SerializeField] GameObject Hanko;

    public void OnClick()
    {
        animator.Play("UI Hanko Animation");
        StartCoroutine(StartDelay());
        Hanko.SetActive(true);
    }
    
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Delay);
        GameManager.Instance.LoadInGameScene();
    }
}

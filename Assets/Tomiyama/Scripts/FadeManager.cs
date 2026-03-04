using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 0.5f;

    [SerializeField]
    private CanvasGroup fadeImage;
    
    public async UniTask FadeIn(Action onComplete)
    {
        fadeImage.interactable = true;
        fadeImage.blocksRaycasts = true;
        float elapsedTime = 0f;
        var alpha = fadeImage.alpha;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.alpha = alpha;
            await UniTask.Yield();
        }
        await UniTask.Yield();
        onComplete?.Invoke();
    }   
    
    public async UniTask FadeOut()
    {
        float elapsedTime = 0f;
        var alpha = fadeImage.alpha;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.alpha = alpha;
            await UniTask.Yield();
        }
        await UniTask.Yield();
        fadeImage.interactable = false;
        fadeImage.blocksRaycasts = false;
    }
}
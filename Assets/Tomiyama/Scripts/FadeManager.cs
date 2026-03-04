using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class FadeManager : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 0.5f;

    [SerializeField]
    private CanvasGroup fadeImage;

    public async UniTask FadeIn(CancellationToken token = default)
    {
        fadeImage.interactable = true;
        fadeImage.blocksRaycasts = true;
        await Fade(0f, 1f, fadeImage, fadeDuration, token);
    }

    public async UniTask FadeOut(CancellationToken token = default)
    {
        await Fade(1f, 0f, fadeImage, fadeDuration, token);
        fadeImage.interactable = false;
        fadeImage.blocksRaycasts = false;
    }

    private static async UniTask Fade(float from, float to, CanvasGroup canvasGroup, float duration,
        CancellationToken token = default)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var alpha = Mathf.Lerp(from, to, Mathf.Clamp01(elapsedTime / duration));
            canvasGroup.alpha = alpha;
            await UniTask.Yield();
            if (token.IsCancellationRequested)
            {
                break;
            }
        }

        await UniTask.Yield();
    }
}
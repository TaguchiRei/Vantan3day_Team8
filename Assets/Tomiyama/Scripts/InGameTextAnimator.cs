using Cysharp.Threading.Tasks;
using UnityEngine;

public class InGameTextAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _beginDelay = 3f;
    [SerializeField] private float _endDelay = 4f;

    public async UniTask Begin()
    {
        _animator.SetTrigger("Begin");
        await UniTask.Delay((int)(_beginDelay * 1000));
    }

    public async UniTask End()
    {
        _animator.SetTrigger("End");
        await UniTask.Delay((int)(_endDelay * 1000));
    }
}

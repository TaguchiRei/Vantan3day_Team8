using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void Press()
    {
        _animator.SetTrigger("Press");
    }

    public void GameEnd()
    {
        _animator.SetBool("HasEnded", true);
    }
}

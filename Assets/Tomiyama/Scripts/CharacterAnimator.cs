using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void Press()
    {
        _animator.SetTrigger("Press");
    }
}

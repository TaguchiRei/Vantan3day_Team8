using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void Press(StampType stampType)
    {
        var triggerName = stampType switch
        {
            StampType.PersonalSeal => "Circle",
            StampType.CompanySeal => "Square",
            _ => ""
        };
        _animator.SetTrigger(triggerName);
    }

    public void GameEnd()
    {
        _animator.SetBool("HasEnded", true);
    }
}

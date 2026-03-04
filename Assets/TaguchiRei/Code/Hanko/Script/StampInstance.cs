using UnityEngine;

public class StampInstance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    public void PressTheStamp(Sprite hankoSprite)
    {
        _spriteRenderer.sprite = hankoSprite;
        _animator.SetTrigger("Stamp");
    }
}
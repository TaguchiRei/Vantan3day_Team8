using UnityEngine;

public class Document : MonoBehaviour
{
    private const int HideOrder = 10;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Animator _animator;
    [SerializeField] SealImpression _seal;

    public void HideDoc(Sprite hankoSprite, bool apply)
    {
        _sprite.sortingOrder += HideOrder;
        if (apply)
        {
            _seal.ShowSeal(hankoSprite);
            _animator.SetTrigger("Stamp");
        }
        else
        {
            _animator.SetTrigger("Destruction");
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
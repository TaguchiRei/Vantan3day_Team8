using TMPro;
using UnityEngine;

public class Document : MonoBehaviour
{
    private const int HideOrder = 10;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private SealImpression _seal;
    [SerializeField] private TextMeshProUGUI[] _texts;

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
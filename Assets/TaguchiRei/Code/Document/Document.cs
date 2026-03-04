using TMPro;
using UnityEngine;

public class Document : MonoBehaviour
{
    private const int HideOrder = 10;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private SealImpression _seal;
    [SerializeField] private TMP_Text[] _texts;

    public void HideDoc(Sprite hankoSprite, bool apply)
    {
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
    
    public void SetText(params string[] text)
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            if (i < text.Length)
            {
                _texts[i].text = text[i];
            }
            else
            {
                _texts[i].text = string.Empty;
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
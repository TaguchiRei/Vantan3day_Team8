using TMPro;
using UnityEngine;

public class Document : MonoBehaviour
{
    private const int HideOrder = 10;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private SpriteRenderer _faceParts;
    [SerializeField] private SpriteRenderer _hairParts;
    [SerializeField] private Animator _animator;
    [SerializeField] private SealImpression _seal;
    [SerializeField] private TMP_Text[] _texts;

    public void HideDoc(Sprite hankoSprite, DocumentAnimationType animType)
    {
        switch (animType)
        {
            case DocumentAnimationType.SpecialEnd:
                _seal.ShowSeal(hankoSprite);
                _animator.SetTrigger("Special");
                break;
            case DocumentAnimationType.Stamp:
                _seal.ShowSeal(hankoSprite);
                _animator.SetTrigger("Stamp");
                break;
            case DocumentAnimationType.Destruction:
                _animator.SetTrigger("Destruction");
                break;
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

    public void SetPhoto(NPCPhotoData photoData)
    {
        _faceParts.sprite = photoData.face;
        _hairParts.sprite = photoData.hair;
        _hairParts.color = photoData.hairColor;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

public enum DocumentAnimationType
{
    SpecialEnd,
    Stamp,
    Destruction
}
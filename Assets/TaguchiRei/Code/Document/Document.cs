using UnityEngine;

public class Document : MonoBehaviour
{
	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] Animator _animator;
	[SerializeField] SealImpression _seal;

	public void ShowDoc(Sprite sprite)
	{
		_sprite.sprite = sprite;
	}

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

	public void DestroyObject()
	{
		Destroy(gameObject);
	}
}

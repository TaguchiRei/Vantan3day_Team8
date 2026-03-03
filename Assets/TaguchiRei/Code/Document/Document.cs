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

	public void HideDoc(Sprite hankoSprite)
	{
		if (hankoSprite != null)
		{
			_seal.ShowSeal(hankoSprite);
		}
	}
}

using UnityEngine;

public class SealImpression : MonoBehaviour
{
	[SerializeField] SpriteRenderer _spriteRenderer;
	public void ShowSeal(Sprite sprite)
	{
		_spriteRenderer.sprite = sprite;
	}
}

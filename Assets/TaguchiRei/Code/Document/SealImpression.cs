using UnityEngine;

public class SealImpression : MonoBehaviour
{
	[SerializeField] SpriteRenderer spriteRenderer;
	public void ShowSeal(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}
}

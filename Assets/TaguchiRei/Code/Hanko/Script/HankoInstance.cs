using UnityEngine;

public class HankoInstance : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Animator _animator;

	public void PressTheStamp(Texture2D hankoTexture)
	{
		_spriteRenderer.sprite = _spriteRenderer.sprite;
		_animator.SetTrigger("Stamp");
	}
}

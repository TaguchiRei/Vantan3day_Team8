using UnityEngine;

public class HankoInstance : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Animator _animator;

	private void Start()
	{
		_spriteRenderer.enabled = false;
	}

	public void PressTheStamp(Texture2D hankoTexture)
	{
		_spriteRenderer.enabled = true;
		_spriteRenderer.sprite = _spriteRenderer.sprite;
		_animator.SetTrigger("Stamp");
	}
}

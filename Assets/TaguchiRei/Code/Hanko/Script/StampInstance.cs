using UnityEngine;

public class StampInstance : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Animator _animator;

	private void Start()
	{
		_spriteRenderer.enabled = false;
	}

	public void PressTheStamp(Sprite hankoSprite)
	{
		_spriteRenderer.enabled = true;
		_spriteRenderer.sprite = hankoSprite;
		_animator.SetTrigger("Stamp");
	}
}

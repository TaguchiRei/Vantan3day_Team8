using System;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void Start()
    {
        var result = GameManager.Instance.GetResult();
    }
}
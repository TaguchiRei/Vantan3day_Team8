using System;
using UnityEngine;

public class Bundle : MonoBehaviour
{
    public Action PlayStartAction;
    public Animator Animator;

    private void PlayStart()
    {
        PlayStartAction?.Invoke();
    }
}
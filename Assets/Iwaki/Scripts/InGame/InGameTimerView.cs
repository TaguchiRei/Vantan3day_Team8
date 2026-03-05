using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameTimerView : MonoBehaviour
{
    [SerializeField] private InGameManager _inGameManager;
    [SerializeField] private Image _image;

    private float _currentTime;

    private void Start()
    {
        _currentTime = _inGameManager.TimeLimit;
        _inGameManager.OnStart += () => StartCoroutine(BeginTimer());
    }

    private IEnumerator BeginTimer()
    {
        while (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;

            _image.fillAmount = _currentTime / _inGameManager.TimeLimit;

            yield return null;
        }
    }
}
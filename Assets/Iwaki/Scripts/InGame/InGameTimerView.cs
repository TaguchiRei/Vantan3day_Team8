using System;
using TMPro;
using UnityEngine;

public class InGameTimerView : MonoBehaviour
{
    [SerializeField] private InGameManager _inGameManager;
    [SerializeField] private TextMeshProUGUI _text;

    private float _currentTime;
    
    private void Start()
    {
        _currentTime = _inGameManager.TimeLimit;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _text.text = TimeSpan.FromSeconds(_currentTime).ToString(@"mm\:ss");
    }
}

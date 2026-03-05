using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private float _gain = 0.5f;

    public void OnShake()
    {
        _cinemachineImpulseSource.GenerateImpulse(_gain);
    }
}

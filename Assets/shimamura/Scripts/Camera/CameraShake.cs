using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;

    public void OnShake()
    {
        _cinemachineImpulseSource.GenerateImpulse();
    }

}

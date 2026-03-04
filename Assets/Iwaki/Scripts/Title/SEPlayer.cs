using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] private SEType type;

    public void Play()
    {
        SoundManager.PlaySE(type);
    }
}

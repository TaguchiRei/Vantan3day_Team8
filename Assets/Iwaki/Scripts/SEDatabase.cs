using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SEDatabase", menuName = "SEDatabase")]
public class SEDatabase : ScriptableObject
{
    [SerializeField] private SEEntry[] _sounds;

    public AudioClip GetSE(SEType type)
    {
        var se = _sounds.FirstOrDefault(x => x.Type == type).Clip;
        if (se == null)
        {
            Debug.LogError($"[SEDatabase] {type} SE がデータベースに存在しません");
        }
        return se;
    }
    
    [Serializable]
    private struct SEEntry
    {
        public SEType Type;
        public AudioClip Clip;
    }
}

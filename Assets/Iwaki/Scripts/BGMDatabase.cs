using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMDatabase", menuName = "BGMDatabase")]
public class BGMDatabase : ScriptableObject
{
    [SerializeField] private BGMEntry[] _sounds;

    public AudioClip GetBGM(BGMType type)
    {
        var bgm = _sounds.FirstOrDefault(x => x.Type == type).Clip;
        if (bgm == null)
        {
            Debug.LogError($"[BGMDatabase] {type} BGM がデータベースに存在しません", this);
        }
        return bgm;
    }
    
    [Serializable]
    private struct BGMEntry
    {
        public BGMType Type;
        public AudioClip Clip;
    }
}

using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class EndingDatabase : ScriptableObject
{
    [SerializeField] private EndingEntry[] _endings;

    public Texture2D GetEnding(EndingType type)
    {
        var ending = _endings.FirstOrDefault(x => x.Type == type).Image;
        return ending;
    }

    [Serializable]
    private struct EndingEntry
    {
        public EndingType Type;
        public Texture2D Image;
    }
}

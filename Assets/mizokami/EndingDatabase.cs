using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu]
public class EndingDatabase : ScriptableObject
{
    [SerializeField] private EndingEntry[] _endings;

    public EndingEntry GetEnding(EndingType type)
    {
        var ending = _endings.FirstOrDefault(x => x.Type == type);
        return ending;
    }
}

[Serializable]
public struct EndingEntry
{
    public EndingType Type;
    public Texture2D Image;
    public string EndingText;
}
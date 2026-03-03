using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hanko", menuName = "Scriptable Objects/Hanko")]
public class StampDataBase : ScriptableObject
{
    public List<StampData> AllStamp = new();
}

[Serializable]
public class StampData
{
    public StampType Type;
    public Sprite Texture;
}

public enum StampType
{
    PersonalSeal,
    CompanySeal,
    Both,
}
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StampDataBase", menuName = "Scriptable Objects/StampDataBase")]
public class StampDataBase : ScriptableObject
{
    public List<StampData> AllStamp = new();
}

[Serializable]
public class StampData
{
    public StampType Type;
    public Sprite MainSprite;
    public Sprite ShadowSprite;
}

public enum StampType
{
    PersonalSeal,
    CompanySeal,
    Both,
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hanko", menuName = "Scriptable Objects/Hanko")]
public class Hanko : ScriptableObject
{
	public List<HankoData> AllHanko = new();
}

public class HankoData
{
	public HankoType Type;
	public Texture2D Texture;
}

public enum HankoType
{
	Hiring,
	NotHiring,
	Personal,
	Thumbprint,
}
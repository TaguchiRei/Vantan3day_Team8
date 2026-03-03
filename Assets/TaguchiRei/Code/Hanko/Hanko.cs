using UnityEngine;

[CreateAssetMenu(fileName = "Hanko", menuName = "Scriptable Objects/Hanko")]
public class Hanko : ScriptableObject
{
    
}

public enum HankoType
{
	Hiring,
	NotHiring,
	Personal,
	Thumbprint,
}
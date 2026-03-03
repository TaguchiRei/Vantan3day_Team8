using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Document/DocumentDateBass")]
public class DocumentDataBase : ScriptableObject
{
    public List<DocumentData> Document = new();
}

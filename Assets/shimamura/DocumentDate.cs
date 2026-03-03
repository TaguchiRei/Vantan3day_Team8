using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Document/DocumentData")]
public class DocumentDate :ScriptableObject
{
    [Header("書類の見た目")]
    public Texture2D ShowDocumentName;

    [Header("書類の名前")]
    public string documentName;

    [Header("ハンコとの書類の対応用")]
    public List<StampRisrutDate> stampEnding = new List<StampRisrutDate>();
}

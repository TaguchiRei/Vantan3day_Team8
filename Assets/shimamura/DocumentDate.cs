using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Document_Database")]
public class DocumentDate :ScriptableObject
{
    [Header("書類の名前")]
    public Texture2D ShowDocumentName;

    [Header("ハンコとの書類の対応用")]
    public List<EndingFlag> stampEnding = new List<EndingFlag>();
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DocumentData
{
    [Header("書類の見た目")]
    public Texture2D ShowDocumentName;

    [Header("ハンコとの書類の対応用")]
    public List<StampResultData> stampEnding = new List<StampResultData>();

}

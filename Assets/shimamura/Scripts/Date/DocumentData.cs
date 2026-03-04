using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class DocumentData
{
    [FormerlySerializedAs("Image")]
    [Header("書類の見た目")] public GameObject Prefab;
    
    [Header("書類のタイプ")] public DocumentType DocumentType;

    [Header("正解のハンコ")] public StampType CorrectStamp;

    [Header("エンディングフラグ")] public EndingFlag EndingFlag;
}

public enum DocumentType
{
    Proposal,
    Resume,
    Marriage,
    Divorce,
    SummonDevil,
}
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DocumentData
{
	[Header("書類の見た目")]
	public Sprite Image;

	[Header("ハンコとの書類の対応用")]
	public List<StampResultData> stampEnding = new();

	public HankoType CorrectHanko;

	public EndingFlag EndingFlag;
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DocumentData
{
	[Header("書類の見た目")]
	public Sprite Image;

	[Header("正解のハンコ")]
	public StampType CorrectStamp;

	[Header("エンディングフラグ")]
	public EndingFlag EndingFlag;
}

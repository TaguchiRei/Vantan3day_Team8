using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Document/DocumentDateBass")]
public class DocumentDataBase : ScriptableObject
{
    public List<DocumentData> Document = new();

    public DocumentData GetRandomDocument()
    {
        int totalWeight = 0;
        foreach (var data in Document)
        {
            totalWeight += data.Weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (var data in Document)
        {
            if (randomValue < data.Weight)
            {
                return data;
            }

            randomValue -= data.Weight;
        }

        Debug.LogWarning("[DocumentDataBase] 書類がデータベースに存在しません", this);
        return null;
    }
}
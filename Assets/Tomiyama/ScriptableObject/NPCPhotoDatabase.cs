using UnityEngine;

[CreateAssetMenu(fileName = "NPCPhotoDatabase", menuName = "Scriptable Objects/NPCPhotoDatabase")]
public class NPCPhotoDatabase : ScriptableObject
{
    [SerializeField]
    private Sprite[] faceSprites;

    [SerializeField]
    private Sprite[] hairSprites;

    public NPCPhotoData GetRandomNPCPhoto()
    {
        if (faceSprites.Length == 0 || hairSprites.Length == 0)
        {
            Debug.LogError("[NPCPhotoDatabase] 顔または髪のスプライトがデータベースに存在しません", this);
            return new NPCPhotoData();
        }

        var randomFaceIndex = Random.Range(0, faceSprites.Length);
        var randomHairIndex = Random.Range(0, hairSprites.Length);
        var randomHairColor = new Color(Random.value, Random.value, Random.value);

        return new NPCPhotoData(faceSprites[randomFaceIndex], hairSprites[randomHairIndex], randomHairColor);
    }
}

public struct NPCPhotoData
{
    public NPCPhotoData (Sprite face, Sprite hair, Color hairColor)
    {
        this.face = face;
        this.hair = hair;
        this.hairColor = hairColor;
    }
    public Sprite face;
    public Sprite hair;
    public Color hairColor;
}
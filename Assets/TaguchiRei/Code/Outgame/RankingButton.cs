using UnityEngine;

public class RankingButton : MonoBehaviour
{
    public void ShowRanking()
    {
        ResultDatabase.Instance.ShowRanking();
    }
}
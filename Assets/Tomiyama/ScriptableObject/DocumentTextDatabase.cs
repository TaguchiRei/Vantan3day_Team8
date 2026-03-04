using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DocumentTextDatabase", menuName = "Scriptable Objects/DocumentTextDatabase")]
public class DocumentTextDatabase : ScriptableObject
{
    [SerializeField]
    private ProposalDocument[] proposalDocuments;

    [SerializeField]
    private ResumeDocument[] resumeDocuments;

    public ProposalDocument GetRandomProposalDocument()
    {
        if (proposalDocuments.Length == 0)
        {
            Debug.LogError("[DocumentTextDatabase] 提案書がデータベースに存在しません", this);
            return new ProposalDocument();
        }

        int randomIndex = UnityEngine.Random.Range(0, proposalDocuments.Length);
        return proposalDocuments[randomIndex];
    }

    public ResumeDocument GetRandomResumeDocument()
    {
        if (resumeDocuments.Length == 0)
        {
            Debug.LogError("[DocumentTextDatabase] 履歴書がデータベースに存在しません", this);
            return new ResumeDocument();
        }

        int randomIndex = UnityEngine.Random.Range(0, resumeDocuments.Length);
        return resumeDocuments[randomIndex];
    }
}

[Serializable]
public struct ResumeDocument
{
    public string UserName => userName;
    public string Gender => gender;
    public string Race => race;
    public string AcademicBackground => academicBackground;
    public string SelfPromotion => selfPromotion;

    [SerializeField]
    [Header("氏名")]
    private string userName;

    [SerializeField]
    [Header("性別")]
    private string gender;

    [SerializeField]
    [Header("種族")]
    private string race;

    [SerializeField]
    [Header("学歴")]
    private string academicBackground;

    [SerializeField]
    [TextArea(3, 10)]
    [Header("自己PR")]
    private string selfPromotion;
}

[Serializable]
public struct ProposalDocument
{
    public string ProposalTitle => proposalTitle;
    public string UserName => userName;
    public string Purpose => purpose;
    public string Cost => cost;

    [SerializeField]
    [Header("提案書のタイトル")]
    private string proposalTitle;

    [SerializeField]
    [Header("氏名")]
    private string userName;

    [SerializeField]
    [TextArea(3, 10)]
    [Header("目的")]
    private string purpose;

    [SerializeField]
    [Header("費用")]
    private string cost;
}
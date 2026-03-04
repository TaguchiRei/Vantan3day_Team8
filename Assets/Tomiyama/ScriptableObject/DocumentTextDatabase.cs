using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DocumentTextDatabase", menuName = "Scriptable Objects/DocumentTextDatabase")]
public class DocumentTextDatabase : ScriptableObject
{
    [SerializeField]
    private ProposalDocument[] proposalDocuments;

    [SerializeField]
    private ResumeDocument[] resumeDocuments;

    public void SetData(ProposalDocument[] proposalDocuments, ResumeDocument[] resumeDocuments)
    {
        this.proposalDocuments = proposalDocuments;
        this.resumeDocuments = resumeDocuments;
    }

    public IDocument GetRandomProposalDocument()
    {
        if (proposalDocuments.Length == 0)
        {
            Debug.LogError("[DocumentTextDatabase] 提案書がデータベースに存在しません", this);
            return new ProposalDocument();
        }

        int randomIndex = UnityEngine.Random.Range(0, proposalDocuments.Length);
        return proposalDocuments[randomIndex];
    }

    public IDocument GetRandomResumeDocument()
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
public struct ResumeDocument : IDocument
{
    public ResumeDocument(string userName, string gender, string race, string academicBackground, string selfPromotion)
    {
        this.userName = userName;
        this.gender = gender;
        this.race = race;
        this.academicBackground = academicBackground;
        this.selfPromotion = selfPromotion;
    }

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

    public string[] GetText()
    {
        return new[] { UserName, Gender, Race, AcademicBackground, SelfPromotion };
    }
}

[Serializable]
public struct ProposalDocument : IDocument
{
    public ProposalDocument(string proposalTitle, string userName, string purpose, string cost)
    {
        this.proposalTitle = proposalTitle;
        this.userName = userName;
        this.purpose = purpose;
        this.cost = cost;
    }

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

    public string[] GetText()
    {
        return new[] { ProposalTitle, UserName, Purpose, Cost };
    }
}

public interface IDocument
{
    string[] GetText();
}
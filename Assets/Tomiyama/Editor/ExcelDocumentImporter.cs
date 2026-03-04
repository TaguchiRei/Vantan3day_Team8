using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
public class ExcelDocumentImporter : EditorWindow
{
    private string resumePath = "";
    private string proposalPath = "";
    private DocumentTextDatabase targetDatabase;

    [MenuItem("Tools/Document Database Importer")]
    public static void Open()
    {
        GetWindow<ExcelDocumentImporter>("Document Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV Settings", EditorStyles.boldLabel);

        DrawFileSelector("Resume CSV", ref resumePath);
        DrawFileSelector("Proposal CSV", ref proposalPath);

        GUILayout.Space(10);

        targetDatabase = (DocumentTextDatabase)EditorGUILayout.ObjectField(
            "Target Database",
            targetDatabase,
            typeof(DocumentTextDatabase),
            false
        );

        GUILayout.Space(20);

        if (GUILayout.Button("Generate / Update Database", GUILayout.Height(40)))
        {
            Import();
        }
    }

    private void DrawFileSelector(string label, ref string path)
    {
        EditorGUILayout.BeginHorizontal();
        path = EditorGUILayout.TextField(label, path);

        if (GUILayout.Button("...", GUILayout.Width(30)))
        {
            string selected = EditorUtility.OpenFilePanel(
                "Select CSV File",
                Application.dataPath,
                "csv"
            );

            if (!string.IsNullOrEmpty(selected))
            {
                path = selected;
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void Import()
    {
        if (string.IsNullOrEmpty(resumePath) || string.IsNullOrEmpty(proposalPath))
        {
            Debug.LogError("CSV path not set.");
            return;
        }

        if (targetDatabase == null)
        {
            Debug.LogError("Target Database not assigned.");
            return;
        }

        var proposalList = LoadProposalCSV(proposalPath);
        var resumeList = LoadResumeCSV(resumePath);

        targetDatabase.SetData(proposalList.ToArray(), resumeList.ToArray());

        EditorUtility.SetDirty(targetDatabase);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Database updated.");
    }

    private static List<ResumeDocument> LoadResumeCSV(string path)
    {
        var list = new List<ResumeDocument>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var values = lines[i].Split(',');

            ResumeDocument doc = new ResumeDocument
            (
                values[0].Trim(),
                values[1].Trim(),
                values[2].Trim(),
                values[3].Trim(),
                values[4].Trim()
            );

            list.Add(doc);
        }

        return list;
    }

    private static List<ProposalDocument> LoadProposalCSV(string path)
    {
        var list = new List<ProposalDocument>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var values = lines[i].Split(',');

            ProposalDocument doc = new ProposalDocument
            (
                values[0].Trim(),
                values[1].Trim(),
                values[2].Trim(),
                values[3].Trim()
            );

            list.Add(doc);
        }

        return list;
    }
}
#endif
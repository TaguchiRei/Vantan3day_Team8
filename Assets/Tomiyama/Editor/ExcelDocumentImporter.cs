using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

#if UNITY_EDITOR
public class DocumentDatabaseImporterWindow : EditorWindow
{
    private string resumePath = "";
    private string proposalPath = "";
    private DocumentTextDatabase targetDatabase;

    [MenuItem("Tools/Document Database Importer")]
    public static void Open()
    {
        GetWindow<DocumentDatabaseImporterWindow>("Document Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV Settings", EditorStyles.boldLabel);

        DrawFileSelector("履歴書のCSVパス", ref resumePath);
        DrawFileSelector("企画書のCSVパス", ref proposalPath);

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

        var resumeList = LoadResumeCSV(resumePath);
        var proposalList = LoadProposalCSV(proposalPath);

        SerializedObject so = new SerializedObject(targetDatabase);

        SetResumeArray(so, resumeList);
        SetProposalArray(so, proposalList);

        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(targetDatabase);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Database updated.");
    }

    private List<ResumeDocument> LoadResumeCSV(string path)
    {
        var list = new List<ResumeDocument>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var values = lines[i].Split(',');

            var doc = new ResumeDocument();
            SetPrivateField(doc, "userName", values[0]);
            SetPrivateField(doc, "gender", values[1]);
            SetPrivateField(doc, "race", values[2]);
            SetPrivateField(doc, "academicBackground", values[3]);
            SetPrivateField(doc, "selfPromotion", values[4]);

            list.Add(doc);
        }

        return list;
    }

    private List<ProposalDocument> LoadProposalCSV(string path)
    {
        var list = new List<ProposalDocument>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var values = lines[i].Split(',');

            var doc = new ProposalDocument();
            SetPrivateField(doc, "proposalTitle", values[0]);
            SetPrivateField(doc, "userName", values[1]);
            SetPrivateField(doc, "purpose", values[2]);
            SetPrivateField(doc, "cost", values[3]);

            list.Add(doc);
        }

        return list;
    }

    private void SetPrivateField(object target, string fieldName, object value)
    {
        var field = target.GetType()
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        field?.SetValue(target, value);
    }

    private void SetResumeArray(SerializedObject so, List<ResumeDocument> list)
    {
        SerializedProperty arrayProp = so.FindProperty("resumeDocuments");
        arrayProp.arraySize = list.Count;

        for (int i = 0; i < list.Count; i++)
        {
            SerializedProperty element = arrayProp.GetArrayElementAtIndex(i);

            element.FindPropertyRelative("userName").stringValue =
                list[i].UserName;

            element.FindPropertyRelative("gender").stringValue =
                list[i].Gender;

            element.FindPropertyRelative("race").stringValue =
                list[i].Race;

            element.FindPropertyRelative("academicBackground").stringValue =
                list[i].AcademicBackground;

            element.FindPropertyRelative("selfPromotion").stringValue =
                list[i].SelfPromotion;
        }
    }

    private void SetProposalArray(SerializedObject so, List<ProposalDocument> list)
    {
        SerializedProperty arrayProp = so.FindProperty("proposalDocuments");
        arrayProp.arraySize = list.Count;

        for (int i = 0; i < list.Count; i++)
        {
            SerializedProperty element = arrayProp.GetArrayElementAtIndex(i);

            element.FindPropertyRelative("proposalTitle").stringValue =
                list[i].ProposalTitle;

            element.FindPropertyRelative("userName").stringValue =
                list[i].UserName;

            element.FindPropertyRelative("purpose").stringValue =
                list[i].Purpose;

            element.FindPropertyRelative("cost").stringValue =
                list[i].Cost;
        }
    }
}
# endif
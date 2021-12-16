
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilitiesGenerator : EditorWindow
{
    private string AbilitiesPath = "Assets/AbilitiesGenerator/Ability.asset";

    private string name;

    private string description;



    [MenuItem("Ability/AbilitiesGenerator")]
    public static void ShowWindow()
    {
        EditorWindow Window = GetWindow<AbilitiesGenerator>("AbilitiesGenerator");
        Window.maxSize = new Vector2(300, 50);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("AbilitiesGenerator", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();
        EditorGUIUtility.labelWidth = 75;
        GUI.SetNextControlName("Ability name : ");
        name = EditorGUILayout.TextField("Ability name : ", name);

        EditorGUIUtility.fieldWidth = (position.width) - 150;
        description = EditorGUILayout.TextField("description : " , description);

        if(GUILayout.Button("Generate Ability"))
        {
            if(isValidContent())
            {
                GenerateAbility();
            }
        }

        EditorGUILayout.EndVertical();
    }

    private bool isValidContent()
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            Debug.LogWarning("You must enter the Ability name");
            return false;
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            Debug.LogWarning("You must enter the Ability description");
            return false;
        }
        return true;
    }

    private void GenerateAbility()
    {
        AbilitiesGenerator a = CreateInstance<AbilitiesGenerator>();

        a.name = name;
        a.description = description;

        string fileName = AssetDatabase.GenerateUniqueAssetPath(AbilitiesPath);

        AssetDatabase.CreateAsset(a, fileName);
        AssetDatabase.SaveAssets();

        ResetUI();

    }

    private void ResetUI()
    {
        name = null;
        description = null;

        GUI.FocusControl("Ability name : ");
    }
   
}

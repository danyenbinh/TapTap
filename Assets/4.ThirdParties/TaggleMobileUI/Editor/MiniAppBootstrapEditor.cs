using UnityEngine;
using UnityEditor;
using TaggleTemplate.Comm;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MiniAppBootstrap))]
public class MiniAppBootstrapEditor : Editor
{
    private void CleanupAPIWorkers()
    {
        foreach (var worker in FindObjectsOfType<APIWorker>())
        {
            DestroyImmediate(worker.gameObject);
        }
    }
    private void LoadSettingsDataForSpeech(MiniAppBootstrap bootStrap)
    {
        APIUnity m_apiUnity = AppBridge.Instance.CreateMainAPI();
        TaggleTemplate.Core.CoroutineHelper.Call(m_apiUnity.LoginAsync(bootStrap.TestUser, bootStrap.TestPassword, (tokenAuth) =>
        {
            Debug.Log("Login successfully. Fetching VidRehab Patient Settings...");
            TaggleTemplate.Core.CoroutineHelper.Call(m_apiUnity.GetVidRehabPatientSettingList((result) =>
            {
                if (result.Success)
                {
                    Debug.Log("Successfully getting setting list");
                    bootStrap.TempSettingsData = result.Data.Select(x => JsonConvert.SerializeObject(x)).ToArray();
                }
                else
                {
                    Debug.Log("Cannot get speech patient setting list");
                }

            }));
        }));
    }
    public override void OnInspectorGUI()
    {
        MiniAppBootstrap miniAppBootstrapScript = (MiniAppBootstrap)target;

        //begin render gui
        serializedObject.Update();

        //render default properties
        DrawPropertiesExcluding(serializedObject, "TempSettingsData");

        //render topbar title
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        //
        Rect rect = EditorGUILayout.GetControlRect(false, 2);
        EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );
        //
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        //
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 15; //change the font size
        guiStyle.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Tool - Pull Setting Datas", guiStyle);
        //
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //render SettingDatas field
		MiniAppBootstrapListEditor.Show(serializedObject.FindProperty("TempSettingsData"), OnSelectItemHandler);
        //
        EditorGUILayout.Space();

        //render bottom action buttons
        EditorGUILayout.BeginHorizontal();
        //        
        if (GUILayout.Button("PullData", EditorStyles.miniButtonLeft, GUILayout.Height(24)))
        {   
            //=========================================//
            // pull new data from dll script
            //=========================================//

            //miniAppBootstrapScript.PullSettingDatas();
            switch (miniAppBootstrapScript.MiniAppLoadType)
            {
                case MiniAppLoadType.VIDCONSULT_REHAB_SPEECH:
                    LoadSettingsDataForSpeech(miniAppBootstrapScript);
                    break;
                default:
                    Debug.Log("Unknown mini app load type. Not handled");
                    break; 

            }

        }
        if (GUILayout.Button("Clear", EditorStyles.miniButtonLeft, GUILayout.Height(24)))
        {
            //=========================================//
            // clear setting datas
            //=========================================//

            ClearSettingDatas();
        }
        //
        EditorGUILayout.EndHorizontal();

        //render bottom action buttons
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Clean up and Save", EditorStyles.miniButtonLeft, GUILayout.Height(24)))
        {
            CleanupAPIWorkers();
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
        EditorGUILayout.EndHorizontal();

        //end render gui
        serializedObject.ApplyModifiedProperties();
    }

    public void OnSelectItemHandler(JObject data)
    {
        //=======================================================================//
        // modify MiniAppBootstrap.SettingDatas property
        //=======================================================================//

        Debug.Log($"Select [{data["id"]}] : { JsonConvert.SerializeObject(data) }");

        MiniAppBootstrap miniAppBootstrapScript = (MiniAppBootstrap)target;
        miniAppBootstrapScript.MiniAppLoadData = JsonConvert.SerializeObject(data);
    }
    
    public void ClearSettingDatas()
    {
        //=======================================================================//
        // modify MiniAppBootstrap.SettingDatas property
        //=======================================================================//

        MiniAppBootstrap miniAppBootstrapScript = (MiniAppBootstrap)target;
        miniAppBootstrapScript.TempSettingsData = new string[]{};
    }
}
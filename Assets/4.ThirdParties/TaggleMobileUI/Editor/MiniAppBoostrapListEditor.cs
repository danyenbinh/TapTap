using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public static class MiniAppBootstrapListEditor
{
	public static void Show (SerializedProperty list, Action<JObject> onSelectItem)
    {
        if(list == null)
        {
            return;
        }

        EditorGUILayout.PropertyField(list);
		EditorGUI.indentLevel += 1;
		for (int i = 0; i < list.arraySize; i++)
        {
            //=======================================================================//
            // render item in list (MiniAppBootstrap.SettingDatas) for select
            //=======================================================================//

            EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));

            if (GUILayout.Button("Select", EditorStyles.miniButtonLeft, GUILayout.Width(128f)))
            {

                JObject jo = JsonConvert.DeserializeObject<JObject>( list.GetArrayElementAtIndex(i).stringValue );

                //TODO: handle parse data of each item in list here
                //...


                //=======================================================================//
                // return item data (JObject) after click select button
                //=======================================================================//
                onSelectItem?.Invoke(jo);
            }

            EditorGUILayout.EndHorizontal();
		}
		EditorGUI.indentLevel -= 1;
	}
}
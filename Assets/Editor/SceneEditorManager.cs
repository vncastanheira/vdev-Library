using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SceneEditorManager : EditorWindow
{
    string[] options = new string[0];
    string[] scenePaths = new string[0];
    int index = 0;

    [MenuItem("Window/Scene Editor Manager")]
	public static void Init()
    {
        var window = GetWindow<SceneEditorManager>();
        window.titleContent = new GUIContent("Scene Select");
        window.ShowUtility();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Scenes", GUI.skin.label);

        var guids = AssetDatabase.FindAssets("t:Scene");
        scenePaths = guids.Select(g => AssetDatabase.GUIDToAssetPath(g)).ToArray();
        options = scenePaths.Select(p => AssetDatabase.LoadAssetAtPath<SceneAsset>(p).name).ToArray();

        index = EditorGUILayout.Popup(index, options);

        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Load"))
        {
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePaths[index]);
            AssetDatabase.OpenAsset(scene);
        }
    }
}

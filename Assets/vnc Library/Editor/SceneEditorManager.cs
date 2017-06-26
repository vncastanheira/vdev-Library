using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace vnc.Editor
{
				public class SceneEditorManager : EditorWindow
				{
								string[] options = new string[0];
								string[] scenePaths = new string[0];

								[MenuItem("Window/vnc Library/Editor Scene Manager")]
								public static void Init()
								{
												var window = GetWindow<SceneEditorManager>();
												window.titleContent = new GUIContent("vnc Scene", VNCStyles.Icon);
												window.Show();
								}

								private void OnGUI()
								{
												EditorGUILayout.Space();
												EditorGUILayout.LabelField("Scenes", EditorStyles.boldLabel);

												var guids = AssetDatabase.FindAssets("t:Scene");
												scenePaths = guids.Select(g => AssetDatabase.GUIDToAssetPath(g)).ToArray();
												options = scenePaths.Select(p => AssetDatabase.LoadAssetAtPath<SceneAsset>(p).name).ToArray();

												var scenesLayoutRect = EditorGUILayout.BeginVertical(EditorStyles.helpBox);
												foreach (var opt in options)
												{
																EditorGUILayout.BeginHorizontal();
																EditorGUILayout.LabelField(opt);
																if (GUI.Button(new Rect(scenesLayoutRect.width - 100, scenesLayoutRect.y, 100, EditorGUIUtility.singleLineHeight), "Open"))
																{
																				var index = options.ToList().IndexOf(opt);
																				var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePaths[index]);
																				AssetDatabase.OpenAsset(scene);
																}
																scenesLayoutRect.y += EditorGUIUtility.singleLineHeight + 2;
																EditorGUILayout.EndHorizontal();
												}
												EditorGUILayout.EndVertical();
								}
				}
}


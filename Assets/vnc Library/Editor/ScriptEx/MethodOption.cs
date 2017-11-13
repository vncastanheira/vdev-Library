using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace vnc.Editor.ScriptEx
{
    [System.Serializable]
    public class MethodOption
    {
        [System.NonSerialized] const int MAX_RANKING = 10;
        [System.NonSerialized] const int LOWEST_RANKING = 0;

        public string Name;
        public int Ranking = 0;
        [System.NonSerialized] public bool Include = false;

        public static MethodOption[] Generate(ScriptType scriptType)
        {
            var options = new List<MethodOption>();
            switch (scriptType)
            {
                case ScriptType.MonoBehaviour:
                    var collection = MonoMethodsFromJson();
                    return collection.methods;
                case ScriptType.ScriptableObject:
                    foreach (var m in scriptableMethods)
                        options.Add(new MethodOption() { Name = m });
                    break;
                case ScriptType.Normal:
                default:
                    break;
            }

            return options.ToArray();
        }

        public static MethodCollection MonoMethodsFromJson()
        {
            var assets = AssetDatabase.FindAssets("mono_methods");
            if (assets.Length == 0)
                throw new System.Exception("Could not found 'mono_methods.json'");

            var monojson = AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(assets[0]));
            MethodCollection collection = JsonUtility.FromJson<MethodCollection>(monojson.text);
            return collection;
        }

        public static void RankMonoMethods(MethodOption[] rankMethods)
        {
            throw new System.NotImplementedException();
        }

        static string[] scriptableMethods =
        {
            "Awake",
            "OnDestroy",
            "OnDisable",
            "OnEnable"
        };
    }

    [System.Serializable]
    public class MethodCollection
    {
        public MethodOption[] methods;
    }
}

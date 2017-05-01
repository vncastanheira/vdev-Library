using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Tools.Localization
{
    /// <summary> A list of registries from specific language. </summary>
    [CreateAssetMenu(fileName = "Language", menuName = "vnclib/Language")]
    public class Language : ScriptableObject
    {
        public string Name;
        public List<RegLang> Registries = new List<RegLang>();
    }

    /// <summary> Registry with a key and the text to be used. </summary>
    [System.Serializable]
    public sealed class RegLang
    {
        public string Key;
        public TextAsset Text;
    }
}


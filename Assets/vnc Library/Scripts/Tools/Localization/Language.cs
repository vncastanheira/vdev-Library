using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Tools.Localization
{
    [CreateAssetMenu(fileName = "Language", menuName = "vnclib/Language")]
    public class Language : ScriptableObject
    {
        public List<RegLang> Registries = new List<RegLang>();
    }

    [System.Serializable]
    public sealed class RegLang
    {
        public string Key;
        public TextAsset Text;
    }
}


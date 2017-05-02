using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using vnc.Utilities;
using System;

namespace vnc.Tools.Localization
{
    /// <summary>
    /// Manages several supported languages in the game
    /// </summary>
    public class LocalizationManager : SingletonMonoBehaviour<LocalizationManager>
    {
        #region Serialized
        /// <summary>Languages registered</summary>
        [Header("Registered Languages"), SerializeField]
        public List<Language> languages = new List<Language>();
        [SerializeField, HideInInspector] int OptionIndex = 0;
        #endregion

        #region Public Properties
        /// <summary>List de language options</summary>
        [HideInInspector] public List<string> DisplayOptions
        {
            get
            {
                return languages.Select(l =>
                {
                    if (l == null)
                        return "Unknown";

                    return l.Name;
                }).ToList();
            }
        }
        /// <summary> Language selected </summary>
        public Language SelectedOption
        {
            get
            {
                if (languages.Count == 0)
                    return null;

                return languages[OptionIndex];
            }
        }

        /// <summary>
        /// Find the text to the corresponding key
        /// </summary>
        /// <param name="key">Key code</param>
        /// <returns>The localized text</returns>
        public string LocalizeText(string key)
        {
            var registry = SelectedOption.Registries
                .FirstOrDefault(r => string.Equals(key, r.Key, StringComparison.CurrentCultureIgnoreCase));
            if(registry != null)
            {
                if (registry.Text == null)
                {
                    Debug.LogError("Localization: key '" + key + "' does not contain a text file.");
                    return string.Empty;
                }

                return registry.Text.text;
            }
            Debug.LogWarning("Localization: key '" + "' not found.");
            return string.Empty;
        }

        #endregion

        #region Private
        [SerializeField, HideInInspector]
        string selectedLanguage;
        #endregion

        #region Singleton
        private void Awake()
        {
            CreateSingleton();
        }

        public override void CreateSingleton()
        {
            Singleton = this;
        }
        #endregion
    }
}


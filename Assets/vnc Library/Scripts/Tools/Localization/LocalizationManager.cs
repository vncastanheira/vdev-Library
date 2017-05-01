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
        /// <summary>List de language options</summary>
        [SerializeField, HideInInspector]
        public List<string> Options
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
        #endregion

        #region Public Properties
        /// <summary>Language selected in game menu</summary>
        public string SelectedLanguage
        {
            get
            {
                return selectedLanguage;
            }

            private set
            {
                selectedLanguage = value;
            }
        }
        #endregion

        #region Private
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


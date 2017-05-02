using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Tools.Localization
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string Key;
        UnityEngine.UI.Text textComponent;

        void Start()
        {
            // Localize the text at the start of the game
            textComponent = GetComponent<UnityEngine.UI.Text>();
            if (LocalizationManager.Singleton != null)
            {
                string localizedText = LocalizationManager.Singleton.LocalizeText(Key);
                if (!string.IsNullOrEmpty(localizedText))
                    textComponent.text = localizedText;
            }
        }
    }
}

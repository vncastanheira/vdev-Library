using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace vnc.Editor
{
    public static class VNCStyles
    {
        public static Texture Icon
        {
            get
            {
                var icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/vnc Library/Icons/favicon.png");
                return icon;
            }
        }

        public static GUIStyle CenteredLabelBold
        {
            get
            {
                var label = GUI.skin.label;
                label.fontStyle = FontStyle.Bold;
                label.alignment = TextAnchor.MiddleCenter;
                return label;
            }
        }

    }
}


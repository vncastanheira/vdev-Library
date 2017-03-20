using UnityEditor;
using UnityEngine;
using vnc.Utilities;

namespace vnc.Editor
{
    [CustomEditor(typeof(CustomQuad))]
    public class CustomQuadEditor : UnityEditor.Editor
    {
        CustomQuad customQuad;

        private void OnEnable()
        {
            customQuad = (CustomQuad) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create"))
            {
                customQuad.Create(customQuad.rect.position, customQuad.rect.size);
            }
        }
    }
}


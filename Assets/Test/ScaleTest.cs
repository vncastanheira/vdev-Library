using UnityEngine;
using vnc.Utilities;

namespace vnc.Test
{
    [ExecuteInEditMode]
    public class ScaleTest : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Scale X"))
            {
                transform.LocalScale(Vector3.right * 3, Vector3.right);
            }
            if (GUILayout.Button("Scale Y"))
            {
                transform.LocalScale(Vector3.up * 3, Vector3.up);
            }
            if (GUILayout.Button("Scale Z"))
            {
                transform.LocalScale(Vector3.forward * 3, Vector3.forward);
            }

            if (GUILayout.Button("Top Right"))
            {
                transform.LocalScale(Vector3.one * 3, Vector3.one);
            }

            if (GUILayout.Button("Bottom Left"))
            {
                transform.LocalScale(Vector3.one * 3, Vector3.one * -1);
            }
        }
    }
}

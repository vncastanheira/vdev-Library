using UnityEngine;

namespace vnc.Utilities
{
    public class SpriteBillboard : MonoBehaviour
    {
        Camera _camera;

        void Start()
        {
            _camera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            if (_camera != null)
            {
                transform.LookAt(Camera.main.transform);
            }
        }
    }
}


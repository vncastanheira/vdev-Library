using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Tools.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {

        #region References
        // Required components for the controller to work
        [Tooltip("When this is enable, any reference for PersonCamera will be ignored.")]
        public bool UseMainCamera = false;
        public Camera PersonCamera;
        CharacterController p_character;
        #endregion

        #region Settings
        // Movement speed
        public float Speed;
        // When in first person mode, cursor is hidden and locked.
        [HideInInspector] public bool FirstPersonMode = true;
        #endregion

        #region Unity Methods
        void Start()
        {
            p_character = GetComponent<CharacterController>();
            if (UseMainCamera)
                PersonCamera = Camera.main;

            if (PersonCamera == null)
                Debug.LogError("Camera is null for First Person Controller in " + name);
        }

        void Update()
        {
            if (PersonCamera == null)
                return;

            CursorMode();

            if (FirstPersonMode)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                Look(mouseX, mouseY);

                var walk = Input.GetAxis("Vertical") * p_character.transform.TransformDirection(Vector3.forward);
                var strafe = Input.GetAxis("Horizontal") * p_character.transform.TransformDirection(Vector3.right);

                p_character.SimpleMove((walk + strafe) * Speed);
            }
        }
        #endregion

        #region region Private Methods
        void Look(float mouseX, float mouseY)
        {
            p_character.transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);
            PersonCamera.transform.localRotation *= Quaternion.Euler(-mouseY, 0f, 0f);
            PersonCamera.transform.localRotation = ClampRotation(PersonCamera.transform.localRotation, -90f, 90f);
        }

        void CursorMode()
        {
            if (FirstPersonMode)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotation(Quaternion q, float minAngle, float maxAngle)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, minAngle, maxAngle);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
        #endregion
    }
}


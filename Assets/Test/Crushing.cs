using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using vnc.Utilities;

namespace vnc.Test
{
    public class Crushing : MonoBehaviour
{
    public LayerMask layerMask;
    BoxCollider box;
    Collider[] colliders;
    Vector3 dir = Vector3.zero;
    float dist = 0;

    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    void Update()
    {
        var n = PhysicsExtensions.OverlapBoxNonAlloc(box, colliders);
        for (int i = 0; i < n; i++)
        {
            var collider = colliders[i];
            if (collider == box)
                continue;

            Physics.ComputePenetration(box, transform.position, transform.rotation,
                collider, collider.transform.position, transform.rotation,
                out dir, out dist);

            return;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Handles.color = Color.white;
            if (dir != Vector3.zero)
                Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(dir), 1.5f, EventType.Repaint);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (dir * dist));
        }
    }

    private void OnGUI()
    {
        var rect = new Rect(0, 0, 200, 50);
        GUI.Label(rect, "Dir: " + dir);
        rect.y += 50;
        GUI.Label(rect, "Dist: " + dist);
    }
}
}

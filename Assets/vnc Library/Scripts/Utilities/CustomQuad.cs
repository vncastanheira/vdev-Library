using System.Linq;
using UnityEngine;

namespace vnc.Utilities
{
    public static class CustomQuad
    {
        public static GameObject Create(Rect rect, Material customMaterial = null)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Mesh mesh = (Mesh) Object.Instantiate(quad.GetComponent<MeshFilter>().sharedMesh);

            var vertices = new Vector3[]
            {
                rect.position,
                rect.position + rect.size,
                new Vector2(rect.position.x + rect.size.x, rect.position.y),
                new Vector2(rect.position.x, rect.position.y + rect.size.y)
            };
            mesh.SetVertices(vertices.ToList());
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            quad.GetComponent<MeshFilter>().mesh = mesh;

            // Fix collider
            Object.DestroyImmediate(quad.GetComponent<MeshCollider>());
            quad.AddComponent<MeshCollider>();

            if (customMaterial != null)
            {
                var renderer = quad.GetComponent<MeshRenderer>();
                renderer.material = customMaterial;
            }

            return quad;
        }
    }
}


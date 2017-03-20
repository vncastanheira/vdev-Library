using System.Linq;
using UnityEngine;

namespace vnc.Utilities
{
    public class CustomQuad : MonoBehaviour
    {
        public Rect rect;
        public Material customMaterial;

        public void Create(Vector2 rectPos, Vector2 rectSize)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Mesh mesh = (Mesh)Instantiate(quad.GetComponent<MeshFilter>().sharedMesh);

            var vertices = new Vector3[]
            {
                rectPos,
                rectPos + rectSize,
                new Vector2(rectPos.x + rectSize.x, rectPos.y),
                new Vector2(rectPos.x, rectPos.y + rectSize.y)
            };
            mesh.SetVertices(vertices.ToList());
            quad.GetComponent<MeshFilter>().mesh = mesh;

            // Fix collider
            DestroyImmediate(quad.GetComponent<MeshCollider>());
            quad.AddComponent<MeshCollider>();

            if (customMaterial != null)
            {
                var renderer = quad.GetComponent<MeshRenderer>();
                renderer.material = customMaterial;
            }
        }
    }
}


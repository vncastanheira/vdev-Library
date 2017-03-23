using System.Linq;
using UnityEngine;

namespace vnc.Utilities
{
    public static class CustomQuad
    {
        /// <summary>
        /// Create a customized Quad from a standart Unity Quad mesh
        /// </summary>
        /// <param name="rect">Position and size</param>
        /// <param name="customMaterial">Apply an user-made material</param>
        /// <returns></returns>
        public static GameObject Create(Rect rect, Material customMaterial = null)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Mesh mesh = (Mesh)Object.Instantiate(quad.GetComponent<MeshFilter>().sharedMesh);

            // Each Quad vertex have x: -0.5 and y: -0.5 units
            Vector2 adjust = (Vector2.one / 2);
            var vertices = new Vector3[]
            {
                rect.position - adjust,
                rect.position + rect.size - adjust,
                new Vector2(rect.position.x + rect.size.x, rect.position.y) - adjust,
                new Vector2(rect.position.x, rect.position.y + rect.size.y) - adjust
            };
            mesh.SetVertices(vertices.ToList());
            mesh.RecalculateBounds();
            quad.GetComponent<MeshFilter>().mesh = mesh;

            // Remove collider
            // TODO: add collider that actually works
            Object.DestroyImmediate(quad.GetComponent<Collider>());

            if (customMaterial != null)
            {
                var renderer = quad.GetComponent<MeshRenderer>();
                renderer.material = customMaterial;
            }

            return quad;
        }
    }
}


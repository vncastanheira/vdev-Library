using UnityEngine;

namespace vnc.Utilities
{
    public static class TransformUtility
    {
        /// <summary>
        /// Scale the object to a certain direction, using current position as a starting point (pivot)
        /// </summary>
        /// <param name="transform">Object transformed.</param>
        /// <param name="size">Size to be scaled.</param>
        /// <param name="scaleDirection">Direction taken. Use negative values to make it go to opposite directions.</param>
        public static void LocalScale(this Transform transform, Vector3 size, Vector3 scaleDirection)
        {
            var originalScale = transform.localScale;
            var pos = transform.localPosition;
            transform.localScale += size;

            pos.x += ((size.x) / 2) * scaleDirection.x;
            pos.y += ((size.y) / 2) * scaleDirection.y;
            pos.z += ((size.z) / 2) * scaleDirection.z;
            
            transform.localPosition = pos;
        }

        /// <summary>
        /// Use a Layer Mask to compare with the object's layer
        /// </summary>
        /// <param name="obj">the object that contains the layer</param>
        /// <param name="layerMask"></param>
        /// <returns>If the object contain the layer specified the layer mask</returns>
        public static bool CompareLayer(this GameObject obj, LayerMask layerMask)
        {
            return ((1 << obj.layer) & layerMask) != 0;
        }
    }

}


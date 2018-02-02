using UnityEngine;

namespace vnc.Utilities
{
    [System.Serializable]
    public class MinMaxRange
    {
        public float rangeStart, rangeEnd;

        public float GetRandomValue()
        {
            return Random.Range(rangeStart, rangeEnd);
        }
    }
}

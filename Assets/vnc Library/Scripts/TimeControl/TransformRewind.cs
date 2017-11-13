using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace vnc.TimeSystem
{
    [System.Serializable]
    public class TransformRewind
    {
        private Transform reference;
        [SerializeField] private List<Vector3> recordedPos;
        [SerializeField] private List<Quaternion> recordedRot;
        [SerializeField] private List<Vector3> recordedScale;

        private Vector3 targetPos;
        private Quaternion targetRot;
        private Vector3 targetScale;

        public TransformRewind(Transform reference)
        {
            this.reference = reference;

            recordedPos = new List<Vector3>(TimeControl.TimeSnapshots + 1);
            recordedRot = new List<Quaternion>(TimeControl.TimeSnapshots + 1);
            recordedScale = new List<Vector3>(TimeControl.TimeSnapshots + 1);
        }

        public void Record()
        {
            recordedPos.Add(reference.position);
            recordedRot.Add(reference.rotation);
            recordedScale.Add(reference.localScale);

            if (recordedPos.Count > TimeControl.TimeSnapshots)
                recordedPos.RemoveAt(0);

            if (recordedRot.Count > TimeControl.TimeSnapshots)
                recordedRot.RemoveAt(0);

            if (recordedScale.Count > TimeControl.TimeSnapshots)
                recordedScale.RemoveAt(0);
        }

        public void Rewind()
        {
            if (recordedPos.Count > 0)
            {
                targetPos = recordedPos.Last();
                recordedPos.Remove(targetPos);
            }

            if (recordedRot.Count > 0)
            {
                targetRot = recordedRot.Last();
                recordedRot.Remove(targetRot);
            }

            if (recordedScale.Count > 0)
            {
                targetScale = recordedScale.Last();
                recordedScale.Remove(targetScale);
            }
            reference.position = targetPos;
            reference.rotation = targetRot;
            reference.localScale = targetScale;
        }
    }
}

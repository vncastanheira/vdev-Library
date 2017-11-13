using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

namespace vnc.TimeSystem
{
    [System.Serializable]
    public class AnimatorRewind
    {
        private Animator reference;
        private List<int> stateHashes;
        public float playbackTime;

        public AnimatorRewind(Animator animator)
        {
            reference = animator;
            stateHashes = new List<int>(TimeControl.TimeSnapshots + 1);
        }

        public void Rewind()
        {
            playbackTime -= Time.fixedDeltaTime;
            playbackTime = Mathf.Clamp(playbackTime, reference.recorderStartTime, reference.recorderStopTime);
            reference.playbackTime = playbackTime;
        }
    }
}

using UnityEngine;

namespace vnc.TimeSystem
{
    public class TimeRewindable : MonoBehaviour
    {
        [Header("Transform")]
        [SerializeField]
        private bool m_rewindTransform = false;
        [SerializeField]
        private TransformRewind transformRewind;

        [Header("Animation"), Space]
        [SerializeField]
        private bool m_rewindAnimation = false;
        private AnimatorRewind animatorRewind;
        private Animator m_animator;
        private AnimatorRecorderMode m_animRecorderMode = AnimatorRecorderMode.Record;

        [Header("Audio"), Space]
        [SerializeField]
        private bool m_rewindAudio = false;
        [SerializeField]
        private AudioRewind audioRewind;

        private void Start()
        {
            if (m_rewindTransform)
                transformRewind = new TransformRewind(transform);

            if (m_rewindAnimation)
            {
                m_animator = GetComponent<Animator>();
                if (m_animator == null)
                {
                    Debug.LogError("Cannot rewind animation without Animator Component");
                }
                else
                {
                    animatorRewind = new AnimatorRewind(m_animator);
                    m_animator.StartRecording(TimeControl.TimeSnapshots);
                }
            }
        }

        public void Record()
        {
            if (m_rewindTransform)
                transformRewind.Record();

            if (m_rewindAudio)
                audioRewind.Record();
        }

        public void Rewind()
        {
            if (m_rewindTransform)
                transformRewind.Rewind();

            if (m_rewindAnimation)
                animatorRewind.Rewind();

            if (m_rewindAudio)
                audioRewind.Rewind();
        }

        public void ToggleAnimatorMode()
        {
            m_animRecorderMode = m_animRecorderMode == AnimatorRecorderMode.Playback 
                ? AnimatorRecorderMode.Record 
                : AnimatorRecorderMode.Playback;
            if (m_rewindAnimation)
            {
                switch (m_animRecorderMode)
                {
                    case AnimatorRecorderMode.Playback:
                        m_animator.StopRecording();
                        m_animator.StartPlayback();
                        animatorRewind.playbackTime = m_animator.recorderStopTime;
                        break;
                    case AnimatorRecorderMode.Record:
                        m_animator.StopPlayback();
                        m_animator.StartRecording(TimeControl.TimeSnapshots);
                        break;
                }
            }
        }

        private void OnGUI()
        {
            var debugRect = new Rect(0, 0, 200, 50);
            GUI.Label(debugRect, "Mode: " + m_animRecorderMode);
            debugRect.y += 50;
            GUI.Label(debugRect, "Playback Time: " + animatorRewind.playbackTime);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace vnc.TimeSystem
{
    public class TimeControl : MonoBehaviour
    {
        [Tooltip("Record time in seconds")]
        public float RecordTime;
        public List<TimeRewindable> rewindableEntities;

        public enum TimeState { Recording, Rewinding }
        public static TimeState State = TimeState.Recording; // Systems current mode

        public static int TimeSnapshots { get; private set; }

        void Awake()
        {
            TimeSnapshots = Mathf.RoundToInt(RecordTime / Time.fixedDeltaTime);
            rewindableEntities = FindObjectsOfType<TimeRewindable>().ToList();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                State = (TimeState)(State == 0 ? 1 : 0);

                foreach (var entity in rewindableEntities)
                    entity.ToggleAnimatorMode();
            }
        }

        private void FixedUpdate()
        {
            switch (State)
            {
                case TimeState.Recording:
                    foreach (var entity in rewindableEntities)
                        entity.Record();
                    break;
                case TimeState.Rewinding:
                    foreach (var entity in rewindableEntities)
                        entity.Rewind();
                    break;
            }
        }
    }
}

using UnityEngine;

namespace vnc.TimeSystem
{
    public class TimeTestPlayer : MonoBehaviour
    {
        public float speed = 8;

        private void Update()
        {
            if(TimeControl.State == TimeControl.TimeState.Recording)
            {
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");
                var move = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
                transform.Translate(move);
            } 
        }
    }
}

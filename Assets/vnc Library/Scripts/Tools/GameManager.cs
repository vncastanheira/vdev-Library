namespace vnc.Tools
{
    public class GameManager
    {
        bool isRunning;
        public bool IsRunning { get { return isRunning; } }

        public void Pause()
        {
            isRunning = false;
        }

        public void UnPause()
        {
            isRunning = true;
        }
    }
}

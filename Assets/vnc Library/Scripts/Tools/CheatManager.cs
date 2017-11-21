using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace vnc.Tools
{
    public static class CheatManager
    {
        /// <summary> Function command </summary>
        public delegate void xcommand();

        public static Dictionary<string, xcommand> Commands { get; private set; }

        static GUISkin skin = null;
        public static GUISkin Skin
        {
            get
            {
                if (skin == null)
                    return GUI.skin;

                return skin;
            }
            set
            {
                skin = value;
            }
        }

        #region Settings

        const int LOG_MAX_SIZE = 10;
        static bool consoleWindowActive;
        static List<string> commandLog;
        static string commandInput;
        static Vector2 scrollPosition = Vector2.zero;

        const float COMMAND_INPUT_HEIGHT = 25;
        const float COMMAND_LOG_HEIGHT = 25;

        static Rect ConsoleGroup
        {
            get
            {
                return new Rect(0, 0, Screen.width, Screen.height / 3);
            }
        }

        static Rect ConsoleLogView
        {
            get
            {
                return new Rect(5, 5, ConsoleGroup.width - 5, ConsoleGroup.height - COMMAND_INPUT_HEIGHT - 5);
            }
        }

        #endregion Settings

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Init()
        {
            commandInput = string.Empty;
            consoleWindowActive = false;
            commandLog = new List<string>();
            Commands = new Dictionary<string, xcommand>();

            new GameObject().AddComponent<CheatManagerBehaviour>();

            skin = Resources.FindObjectsOfTypeAll<GUISkin>().SingleOrDefault(s => s.name == "CheatsGUISkin");
        }

        /// <summary>
        /// Register a new command
        /// </summary>
        /// <param name="key">Key to be accessed</param>
        /// <param name="command">Function to execute</param>
        public static void RegisterCommand(string key, xcommand command)
        {
            Commands.Add(key, command);
        }

        /// <summary>
        /// Command to be executed
        /// </summary>
        /// <param name="command">Command parameter</param>
        public static void Run(string key)
        {
            xcommand cmd;
            if (Commands.TryGetValue(key, out cmd))
            {
                LogCommand(string.Format("'{0}' activated.", key));
                cmd();
            }
            else
            {
                LogCommand(string.Format("'{0}' not found.", key));
            }
        }

        /// <summary>
        /// Create a entry log
        /// </summary>
        /// <param name="command">Command logged</param>
        public static void LogCommand(string command)
        {
            commandLog.Add(command);
            if (commandLog.Count > LOG_MAX_SIZE)
                commandLog.RemoveAt(0);
            else
                scrollPosition = new Vector2(0, scrollPosition.y + COMMAND_LOG_HEIGHT);

            commandInput = string.Empty;
        }

        public static void OnGUI()
        {
            // CAPTURE KEY
            Event key = Event.current;
            if (key.type == EventType.keyDown)
            {
                if (key.keyCode == KeyCode.Return && !string.IsNullOrEmpty(commandInput))
                {
                    Run(commandInput);
                }
                else if (key.keyCode == KeyCode.Quote || key.keyCode == KeyCode.BackQuote)
                {
                    commandInput = string.Empty;
                    consoleWindowActive = !consoleWindowActive;
                }
            }

            // DRAW
            if (consoleWindowActive)
            {
                GUI.BeginGroup(ConsoleGroup, Skin.scrollView);
                scrollPosition = GUI.BeginScrollView(ConsoleLogView, scrollPosition, new Rect(0, 0, 100, COMMAND_INPUT_HEIGHT * commandLog.Count));
                for (int i = commandLog.Count - 1; i >= 0; i--)
                    GUI.Label(new Rect(0, i * 20, Screen.width, COMMAND_LOG_HEIGHT), commandLog[i], Skin.label);

                GUI.EndScrollView();
                Rect textFieldBox = new Rect(0, ConsoleLogView.height, Screen.width, COMMAND_INPUT_HEIGHT);
                commandInput = GUI.TextField(textFieldBox, commandInput, 25, Skin.textField);
                GUI.EndGroup();
            }
        }
    }

    class CheatManagerBehaviour : MonoBehaviour
    {
        private void OnGUI()
        {
            CheatManager.OnGUI();
        }
    }

    [System.Serializable]
    public sealed class Cheat
    {
        [SerializeField] public string Key;
        [SerializeField] public UnityEvent Event;
    }

    [System.Serializable]
    public static class CheatListExtension
    {
        public static bool TryGetValue(this List<Cheat> list, string key, out UnityEvent value)
        {
            var cheat = list.FirstOrDefault(c => string.Equals(key, c.Key, System.StringComparison.CurrentCultureIgnoreCase));
            if (cheat != null)
            {
                value = cheat.Event;
                return true;
            }

            value = null;
            return false;
        }
    }
}

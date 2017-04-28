using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CheatManager : MonoBehaviour
{
    public bool DeveloperConsole;
    public List<Cheat> Cheats;

    [Header("Customization")]
    public GUISkin Skin;

    #region Settings

    bool consoleWindowActive;
    const int LOG_MAX_SIZE = 10;
    List<string> commandLog;
    string commandInput;
    Vector2 scrollPosition = Vector2.zero;
    #endregion

    #region DEBUG
#if UNITY_EDITOR
    [Header("Debug Settings")]
    public bool DebugActive; // Start with the console window active
#endif
    #endregion

    #region Calculations & Constants
    public const float COMMAND_INPUT_HEIGHT = 25;
    public const float COMMAND_LOG_HEIGHT = 25;
    public Rect ConsoleGroup
    {
        get
        {
            return new Rect(0, 0, Screen.width, Screen.height / 3);
        }
    }
    public Rect ConsoleLogView
    {
        get
        {
            return new Rect(5, 5, ConsoleGroup.width - 5, ConsoleGroup.height - COMMAND_INPUT_HEIGHT - 5);
        }
    }
    #endregion

    #region Default Settings
    void Awake()
    {
        commandInput = string.Empty;
        consoleWindowActive = false;
        commandLog = new List<string>();
        if (Cheats == null)
            Cheats = new List<Cheat>();
#if UNITY_EDITOR
        consoleWindowActive = DebugActive;
#endif
    }

    #endregion
    /// <summary>
    /// Command to be executed
    /// </summary>
    /// <param name="command">Command parameter</param>
    void ExecuteCommand(string command)
    {
        UnityEvent invoker;
        if (Cheats.TryGetValue(command, out invoker))
        {
            LogCommand(string.Format("'{0}' activated.", command));
            invoker.Invoke();
        }
        else
        {
            LogCommand(string.Format("'{0}' not found.", command));
        }
    }

    /// <summary>
    /// Create a entry log
    /// </summary>
    /// <param name="command">Command logged</param>
    void LogCommand(string command)
    {
        commandLog.Add(command);
        if (commandLog.Count > LOG_MAX_SIZE)
            commandLog.RemoveAt(0);
        else
            scrollPosition = new Vector2(0, scrollPosition.y + COMMAND_LOG_HEIGHT);

        commandInput = string.Empty;
    }

    void OnGUI()
    {
        // CAPTURE KEY
        Event key = Event.current;
        if (key.type == EventType.keyDown && DeveloperConsole)
        {
            if (key.keyCode == KeyCode.Return && !string.IsNullOrEmpty(commandInput))
            {
                ExecuteCommand(commandInput);
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
        if(cheat != null)
        {
            value = cheat.Event;
            return true;
        }

        value = null;
        return false;
    }
}
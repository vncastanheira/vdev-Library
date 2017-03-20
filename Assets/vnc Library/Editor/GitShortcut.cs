#if UNITY_EDITOR
using System.Diagnostics;
using UnityEditor;

namespace vnc.Editor
{
    public class GitShortcut : EditorWindow
    {
	    [MenuItem("Window/vnc Library/Git Bash")]
	    public static void ShowBash()
	    {
		    Process foo = new Process();
		    foo.StartInfo.FileName = @"C:\Program Files\Git\git-bash.exe";
		    foo.Start();
	    }
    }
}
#endif
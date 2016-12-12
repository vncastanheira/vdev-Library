#if UNITY_EDITOR
using UnityEngine;
using System.Diagnostics;
using UnityEditor;

public class GitShortcut : EditorWindow
{
	[MenuItem("Git/Open Bash", priority = 1)]
	public static void ShowBash()
	{
		Process foo = new Process();
		foo.StartInfo.FileName = @"C:\Program Files\Git\git-bash.exe";
		foo.Start();
	}
}
#endif
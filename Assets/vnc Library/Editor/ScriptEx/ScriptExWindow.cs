using System.Linq;
using UnityEditor;
using UnityEngine;

namespace vnc.Editor.ScriptEx
{
    public class ScriptExWindow : EditorWindow
    {
        #region Initialization
        public static ScriptExWindow window = null;

        [MenuItem("Assets/New ScriptEx")]
        static void Init()
        {
            var iconpath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("scriptex")[0]);
            var icon = AssetDatabase.LoadAssetAtPath<Texture>(iconpath);

            window = GetWindow<ScriptExWindow>();
            window.titleContent = new GUIContent("SCRIPT EX", icon);
            window.Show();
            window.Focus();
        }
        #endregion

        ScriptTemplate currentTemplate;
        ScriptType scriptType;
        MethodOption[] monoMethods;
        MethodOption[] scriptableMethods;

        #region Window Settings
        Vector2 scrollPos;
        bool closeOnConfirm = false;
        string warningBox;
        string methodFilter;
        Texture header;
        #endregion

        private void OnEnable()
        {
            var headerpath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("scriptex-header")[0]);
            header = AssetDatabase.LoadAssetAtPath<Texture>(headerpath);

            currentTemplate = new ScriptTemplate();
            monoMethods = MethodOption.Generate(ScriptType.MonoBehaviour);
            scriptableMethods = MethodOption.Generate(ScriptType.ScriptableObject);
            methodFilter = string.Empty;
        }

        private void OnGUI()
        {
            MethodOption.MonoMethodsFromJson();

            EditorGUILayout.LabelField(new GUIContent(header), EditorStyles.helpBox);

            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            currentTemplate.Name = EditorGUILayout.TextField("Name", currentTemplate.Name);
            if (!string.IsNullOrEmpty(warningBox))
                EditorGUILayout.HelpBox(warningBox, MessageType.Error);

            scriptType = (ScriptType)EditorGUILayout.EnumPopup("Script Type", scriptType);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(SmallBox(this.position.width));
            if (GUILayout.Button("Create Script"))
            {
                if (currentTemplate.Validate(out warningBox))
                {
                    CreateScript();
                    AssetDatabase.Refresh();
                    if (closeOnConfirm)
                        this.Close();
                }
            }
            closeOnConfirm = EditorGUILayout.ToggleLeft("Close on Confirm?", closeOnConfirm);
            EditorGUILayout.EndVertical();



            EditorGUILayout.Space();

            // METHOD LISTING
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            if (scriptType == ScriptType.MonoBehaviour || scriptType == ScriptType.ScriptableObject)
            {
                EditorGUILayout.LabelField("Methods", EditorStyles.boldLabel);
                methodFilter = EditorGUILayout.TextField("Search: ", methodFilter);
                System.Collections.Generic.IEnumerable<MethodOption> filteredOptions;
                if (string.IsNullOrEmpty(methodFilter))
                {
                    switch (scriptType)
                    {
                        case ScriptType.MonoBehaviour:
                            filteredOptions = monoMethods.Where(o => o.Name.ToUpper().Contains(methodFilter.ToUpper()));
                            break;
                        case ScriptType.ScriptableObject:
                            filteredOptions = scriptableMethods.Where(o => o.Name.ToUpper().Contains(methodFilter.ToUpper()));
                            break;
                        default:
                        case ScriptType.Normal:
                            filteredOptions = new System.Collections.Generic.List<MethodOption>();
                            break;
                    }
                }
                else
                {
                    switch (scriptType)
                    {
                        case ScriptType.MonoBehaviour:
                            filteredOptions = monoMethods;
                            break;
                        case ScriptType.ScriptableObject:
                            filteredOptions = scriptableMethods;
                            break;
                        default:
                        case ScriptType.Normal:
                            filteredOptions = new System.Collections.Generic.List<MethodOption>();
                            break;
                    }
                }
                filteredOptions = filteredOptions.OrderByDescending(o => o.Ranking);
                foreach (var option in filteredOptions)
                {
                    option.Include = EditorGUILayout.ToggleLeft(option.Name, option.Include);
                }
            }
            EditorGUILayout.EndScrollView();
        }


        #region Functions
        private void CreateScript()
        {
            // Get path
            var folderPath = FolderPath();
            var path = System.IO.Path.Combine(folderPath, currentTemplate.Name + ".cs");
            // Load template
            var script = GetTemplate(scriptType);
            script = script.Replace("{classname}", currentTemplate.Name);
            script = script.Replace("{methods}", CreateMethodList());

            System.IO.File.WriteAllText(path, script);
        }

        private string CreateMethodList()
        {
            string methodlist = string.Empty;
            if (scriptType == ScriptType.MonoBehaviour)
            {
                var onMethods = monoMethods.Where(o => o.Include);
                foreach (var m in onMethods)
                {
                    methodlist += ("\tvoid " + m.Name + "()\t\n{\t\n\n}\n\n");
                }
            }
            else if (scriptType == ScriptType.ScriptableObject)
            {
                var onMethods = scriptableMethods.Where(o => o.Include);
                foreach (var m in onMethods)
                {
                    methodlist += ("\tvoid " + m.Name + "()\t\n{\t\n\n}\n\n");
                }
            }
            return methodlist;
        }

        public string GetTemplate(ScriptType stype)
        {
            string[] assets = new string[0];
            switch (stype)
            {
                case ScriptType.MonoBehaviour:
                    assets = AssetDatabase.FindAssets("MonoBehaviourTemplate");
                    break;
                case ScriptType.ScriptableObject:
                    assets = AssetDatabase.FindAssets("ScriptableObjectTemplate");
                    break;
                case ScriptType.Normal:
                default:
                    assets = AssetDatabase.FindAssets("NormalTemplate");
                    break;
            }

            if (assets.Length == 0)
                return "";

            var path = AssetDatabase.GUIDToAssetPath(assets[0]);
            var template = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            return template.text;
        }

        public string FolderPath()
        {
            string path = "Assets/";
            foreach (var obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetOrScenePath(obj);
                if (System.IO.File.Exists(path))
                {
                    path = System.IO.Path.GetDirectoryName(path);
                    break;
                }
            }
            return path.Replace("Assets", Application.dataPath);
        }

        public void SetWarning(string text)
        {
            warningBox = text;
        }
        #endregion

        #region Etc
        public static GUIStyle SmallBox(float width)
        {
            var style = new GUIStyle(EditorStyles.helpBox);
            style.margin = new RectOffset(((int)width) - 200, 0, 0, 0);
            style.fixedWidth = 200;
            style.alignment = TextAnchor.MiddleRight;
            return style;
        }

        public static Rect UnityWindowPosition()
        {
            var process = System.Diagnostics.Process.GetProcessesByName("Unity");
            var unitywinrect = new WinRect();
            GetWindowRect(process[0].MainWindowHandle, ref unitywinrect);
            return new Rect(0, 0, 1900, 1600);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr FindWindow(string strClassName, string strWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool GetWindowRect(System.IntPtr hwnd, ref WinRect rectangle);

        public struct WinRect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        #endregion
    }

    public class ScriptTemplate
    {
        public string Name;

        public bool Validate(out string warningText)
        {
            if (string.IsNullOrEmpty(Name))
            {
                warningText = "Name field cannot be empty.";
                return false;
            }

            warningText = string.Empty;
            return true;
        }
    }
}

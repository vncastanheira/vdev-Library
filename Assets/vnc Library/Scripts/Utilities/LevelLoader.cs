using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace vnc.Utilities
{
    /// <summary> Can load your levels and show progress </summary>
    public class LevelLoader : MonoBehaviour
    {
        /// <summary> Show the progress of the current level being loaded </summary>
        [HideInInspector] public float progress;

        /// <summary> Loads a level asyncronously </summary>
        /// <param name="sceneName">Name of the Scene</param>
        public void Load(string sceneName)
        {
            StartCoroutine(LoadAsync(sceneName));
        }

        IEnumerator LoadAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                progress = Mathf.Clamp01(operation.progress / .9f);
                yield return null;
            }

            progress = 0;
        }
    }
}


using UnityEngine;

namespace vnc.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIGroup : MonoBehaviour
    {
        private CanvasGroup group;

        public bool hideOnStart = false;

        void Awake()
        {
            group = GetComponent<CanvasGroup>();
            if (hideOnStart) Hide();
            else Show();
        }

        public void Show()
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        public void Hide()
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }

    }
}

using UnityEngine;

namespace vnc.UI
{
				[RequireComponent(typeof(CanvasGroup))]
				public class UIDialog : MonoBehaviour
				{
								private CanvasGroup group;
								public bool hideOnStart = false;

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

								public void Start()
								{
												group = GetComponent<CanvasGroup>();
												if (hideOnStart)
																Hide();
												else
																Show();
								}
				}
}

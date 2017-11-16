using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vnc.Tools;

public class VncEventTesting : MonoBehaviour, IVncEventListener<ButtonEvent>
{

    Button btn;
    Text btnText;

    void Start()
    {
        this.Listen();
        btn = GetComponent<Button>();
        btnText = btn.GetComponentInChildren<Text>();

        btn.onClick.AddListener(() =>
        {
            VncEventSystem.Trigger(new ButtonEvent { Message = "It werks" });
            this.Unlisten();
        });
    }


    public void OnVncEvent(ButtonEvent e)
    {
        btnText.text = e.Message;
    }
}

public struct ButtonEvent
{
    public string Message;
}

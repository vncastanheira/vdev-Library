using System;
using UnityEngine;

namespace vnc.Experimental.Behaviours
{
    [System.Serializable]
    public enum ConnectionPointType { In, Out }

    [System.Serializable]
    public class ConnectionPoint
    {
        public Rect rect;
        public ConnectionPointType type;
        public Node node;
        [NonSerialized] public GUIStyle style;

        public ConnectionPoint(Node node, ConnectionPointType type)
        {
            this.node = node;
            this.type = type;
            rect = new Rect(0, 0, 10f, 20f);
        }

        public void Draw()
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

            switch (type)
            {
                case ConnectionPointType.In:
                    rect.x = node.rect.x - rect.width + 8f;
                    if (GUI.Button(rect, "", NodeBasedEditor.inPointStyle))
                    {
                        NodeBasedEditor.OnClickInPoint(this);
                    }
                    break;

                case ConnectionPointType.Out:
                    rect.x = node.rect.x + node.rect.width - 8f;
                    break;
            }
        }
    }
}


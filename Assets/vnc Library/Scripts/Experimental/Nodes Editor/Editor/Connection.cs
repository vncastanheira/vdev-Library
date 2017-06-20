using UnityEditor;
using UnityEngine;

namespace vnc.Experimental.Behaviours
{
    [System.Serializable]
    public class Connection
    {
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
        }

        public void Draw()
        {
            Handles.DrawBezier(
                inPoint.rect.center,
                outPoint.rect.center,
                inPoint.rect.center + Vector2.left * 50f,
                outPoint.rect.center - Vector2.left * 50f,
                Color.white,
                null,
                2);

            if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
            {
                NodeBasedEditor.OnClickRemoveConnection(this);
            }
        }
    }
}


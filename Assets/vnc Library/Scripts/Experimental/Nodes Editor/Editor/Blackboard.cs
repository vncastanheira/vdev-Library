using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace vnc.Experimental.Behaviours
{
    [CreateAssetMenu(menuName = "vnclib/Experimental/Blackboard")]
    public class Blackboard : ScriptableObject
    {
        public string Title;
        public List<Node> nodes;
        public List<Connection> connections;
        public List<Parameter> Parameters;
        public AnimatorUpdateMode Update;

        public Blackboard()
        {
            Parameters = new List<Parameter>();
            nodes = new List<Node>();
            connections = new List<Connection>();
        }

        public void SetValue(string name, object value, ParameterType type)
        {
            if(Parameters != null)
            {
                var param = Parameters.FirstOrDefault(p => p.Name.Equals(name));
                if(param.Type != type)
                {
                    Debug.Log("Blackboard: Invalid (" + type.ToString() + ") for parameter " + name);
                    return;

                }
            }
        }
    }
}

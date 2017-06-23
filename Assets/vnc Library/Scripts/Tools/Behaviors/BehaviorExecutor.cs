using FluentBehaviourTree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace vnc.Tools.Behaviors
{
    public abstract class BehaviorExecutor : MonoBehaviour
    {
        public BehaviourTreeBuilder builder { get; private set; }
        public IBehaviourTreeNode updateTree;
        public IBehaviourTreeNode fixedUpdateTree;
        public AnimatorUpdateMode update;

        private List<ParamAttribute> attributes;

        private void Start()
        {
            builder = new BehaviourTreeBuilder();
            CreateAttributes();
            OnStart();
        }

        private void FixedUpdate()
        {
            if(fixedUpdateTree != null)
                fixedUpdateTree.Tick(new TimeData(Time.fixedDeltaTime));
        }

        private void Update()
        {
            if (updateTree != null)
                updateTree.Tick(new TimeData(Time.deltaTime));
        }

        public abstract void OnStart();

        private void CreateAttributes()
        {
            var fields = GetType().GetFields();
            attributes = new List<ParamAttribute>();
            foreach (var f in fields)
            {
                var attr = System.Attribute.GetCustomAttribute(f, typeof(ParamAttribute)) as ParamAttribute;
                if (attr != null)
                {
                    attr.value = f.GetValue(this);
                    attributes.Add(attr);
                }
            }
        }

        public void SetParameter(string name, object value)
        {
            if (attributes != null)
            {
                var param = attributes.SingleOrDefault(p => p.name.Equals(name));
                if (param == null)
                {
                    Debug.LogError("Parameter " + name + " was not found.");
                    return;
                }

                param.value = value;
            }
        }

        public T GetParameter<T>(string name)
        {
            if (attributes != null)
            {
                var param = attributes.SingleOrDefault(p => p.name.Equals(name));
                if (param == null)
                {
                    Debug.LogError("Parameter " + name + " was not found.");
                    return default(T);
                }
                return (T)param.value;
            }
            return default(T);
        }
    }
}

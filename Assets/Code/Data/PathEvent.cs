using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Data
{
    [CreateAssetMenu(fileName = "PathEvent", menuName = "ScriptableObjects/PathEvent", order = 1)]
    public class PathEvent : ScriptableObject
    {
        private List<Action<PathBehaviour>> listeners = new List<Action<PathBehaviour>>();

        public void Raise(PathBehaviour target)
        {
            foreach (var listener in listeners)
            {
                listener.Invoke(target);
            }
        }

        public void AddListeners(Action<PathBehaviour> action)
        {
            listeners.Add(action);
        }

        public void RemoveListeners(Action<PathBehaviour> action)
        {
            listeners.Remove(action);
        }
    }
}

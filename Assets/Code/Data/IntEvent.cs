using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Data
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = "ScriptableObjects/IntEvent", order = 1)]
    public class IntEvent : ScriptableObject
    {
        private List<Action<int>> listeners = new List<Action<int>>();

        public void Raise(int amount)
        {
            foreach (var listener in listeners)
            {
                listener.Invoke(amount);
            }
        }

        public void AddListeners(Action<int> action)
        {
            listeners.Add(action);
        }

        public void RemoveListeners(Action<int> action)
        {
            listeners.Remove(action);
        }
    }
}

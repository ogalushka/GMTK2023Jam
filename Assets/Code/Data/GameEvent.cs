using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Data
{
    [CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 1)]
    public class GameEvent : ScriptableObject
    {
        private List<Action> listeners = new List<Action>();

        public void Raise()
        {
            foreach (var listener in listeners)
            {
                listener.Invoke();
            }
        }

        public void AddListeners(Action action)
        {
            listeners.Add(action);
        }

        public void RemoveListeners(Action action)
        {
            listeners.Remove(action);
        }
    }
}

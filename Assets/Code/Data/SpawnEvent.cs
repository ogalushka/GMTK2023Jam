using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Data
{
    [CreateAssetMenu(fileName = "SpawnWaveEvent", menuName = "ScriptableObjects/SpawnWaveEvent", order = 1)]
    public class SpawnEvent : ScriptableObject
    {
        private List<Action<Building>> listeners = new List<Action<Building>>();

        public void Raise(Building target)
        {
            foreach (var listener in listeners)
            {
                listener.Invoke(target);
            }
        }

        public void AddListeners(Action<Building> action)
        {
            listeners.Add(action);
        }

        public void RemoveListeners(Action<Building> action)
        {
            listeners.Remove(action);
        }
    }
}

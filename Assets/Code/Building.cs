using Assets.Code.Data;
using System;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Collider))]
    public class Building : MonoBehaviour
    {
        public int health;
        public SpawnEvent spawnedEvent;

        public void DealDamage(int amount)
        {
            health -= amount;
            if (!IsAlive())
            {
                gameObject.SetActive(false);
            }
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        private void OnMouseOver()
        {
        }

        private void OnMouseDown()
        {
            spawnedEvent.Raise(this);
        }
    }
}

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
                //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                //gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        private void OnMouseOver()
        {
            gameObject.GetComponent<Light>().intensity = 1f;
        }

        private void OnMouseExit()
        {
            gameObject.GetComponent<Light>().intensity = 0f;
        }

        private void OnMouseDown()
        {
            spawnedEvent.Raise(this);
        }
    }
}

using Assets.Code.Data;
using System;
using UnityEngine;

namespace Assets.Code
{
    public class Unit : MonoBehaviour
    {
        public float speed;
        public float distanceOnLane;
        public int health;
        public GameEvent unitDied;

        public PathBehaviour lane;
        public int damage;

        public void SetPath(PathBehaviour lane)
        {
            this.lane = lane;
            transform.position = lane.First().position;
        }

        public void DealDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
                unitDied.Raise();
            }
        }

        private void Update()
        {
            if (lane == null)
            {
                Debug.LogError($"Lane must be set on an unit");
                return;
            }

            distanceOnLane += speed * Time.deltaTime;

            var moveResult = lane.GetPositionAt(distanceOnLane);
            var passedBuilding = lane.GetPassedBuilding(distanceOnLane);

            if (passedBuilding != null)
            {
                passedBuilding.DealDamage(1);
                Destroy(gameObject);
                unitDied.Raise();
                return;
            }

            transform.position = moveResult;
        }
    }
}

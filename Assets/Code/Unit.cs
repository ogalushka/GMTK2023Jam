using UnityEngine;

namespace Assets.Code
{
    public class Unit : MonoBehaviour
    {
        public float speed;
        public float distanceOnLane;

        public PathBehaviour lane;

        public void SetPath(PathBehaviour lane)
        {
            this.lane = lane;
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
                return;
            }

            transform.position = moveResult;
        }
    }
}

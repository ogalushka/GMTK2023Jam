using UnityEngine;

namespace Assets.Code
{
    public class Tower : MonoBehaviour
    {
        public float range;
        public float shootCooldown;
        public float projectileSpeed;
        public GameObject unitsContainer;

        public Projectile projectilePrefab;

        private float currentRateCooldown;

        private void Update()
        {
            currentRateCooldown -= Time.deltaTime;
            if (currentRateCooldown <= 0)
            {
                if (TryShoot())
                {
                    currentRateCooldown = shootCooldown;
                }
            }
        }

        private bool TryShoot()
        {
            var units = unitsContainer.GetComponentsInChildren<Unit>();
            var rangeSq = range * range;
            Unit currentTarget = null;

            foreach (var unit in units)
            {
                var inRange = (unit.transform.position - transform.position).sqrMagnitude <= rangeSq;
                var isBetterTarget = (currentTarget == null || currentTarget.distanceOnLane < unit.distanceOnLane);

                if (inRange && isBetterTarget)
                {
                    currentTarget = unit;
                }
            }

            if (currentTarget != null)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.SetTarget(currentTarget);
                return true;
            }

            return false;
        }
    }
}

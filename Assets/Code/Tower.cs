using UnityEngine;

namespace Assets.Code
{
    public class Tower : MonoBehaviour
    {
        public float range;
        public float baseShootCooldown;
        public float baseProjectileSpeed;
        public int baseDamage;
        public int baseCost;
        public GameObject unitsContainer;

        public Projectile projectilePrefab;

        private float currentRateCooldown;

        private int threatLevel = 0;
        private int level = 1;

        private float shootCooldown;
        private float projectileSpeed;
        private int damage;
        private int upgradeCost;



        private void Update()
        {
            shootCooldown = baseShootCooldown / level;
            projectileSpeed = baseProjectileSpeed * level;
            damage = baseDamage * level;
            upgradeCost = baseCost * (level + 1);

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
                projectile.SetDamage(damage);
                projectile.speed = projectileSpeed;
                return true;
            }

            return false;
        }

        public void RaiseThreat(int threat)
        {
            threatLevel += threat;
        }

        public void UpgradeTower()
        {
            level++;
        }

        public int GetThreat()
        {
            return threatLevel;
        }


        public void ResetThreat()
        {
            threatLevel = 0;
        }

        public int GetUpgradeCost()
        {
            return upgradeCost;
        }
    }

}

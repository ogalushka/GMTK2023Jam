using UnityEngine;

namespace Assets.Code
{
    public class Projectile : MonoBehaviour
    {
        public float speed;

        private Unit target;

        private int damage =0;

        public void SetTarget(Unit target)
        {
            this.target = target;
        }

        public void SetDamage(int newDamage)
        {
            damage = newDamage;
        }

        private void Update()
        {
            if (this.target == null)
            {
                Destroy(gameObject);
                return;
            }

            var delta = target.transform.position - transform.position;
            var distanceToTarget = delta.magnitude;
            var speedThisFrame = speed * Time.deltaTime;

            if (distanceToTarget < speedThisFrame)
            {
                target.DealDamage(damage);
                Destroy(gameObject);
                return;
            }

            var timeEstimate = distanceToTarget / speed;
            var positionEstimate = target.speed * timeEstimate + target.distanceOnLane;
            var futureTargetPosition = target.lane.GetPositionAt(positionEstimate);

            var directionToTarget = (futureTargetPosition - transform.position).normalized;
            transform.position += directionToTarget * speedThisFrame;
        }
    }
}

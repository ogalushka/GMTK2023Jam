using UnityEngine;

namespace Assets.Code
{
    public class Building : MonoBehaviour
    {
        public int health;

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
    }
}

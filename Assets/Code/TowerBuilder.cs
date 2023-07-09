using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{

    public class TowerBuilder : MonoBehaviour
    {

        [SerializeField] private Tower[] towers;

        
        public float waitForBuild = 0f;
        private float buildTimer = 0f;

        public int aiStartGold = 100;
        public int gold;

        private void Start()
        {
            buildTimer = waitForBuild;
            gold = aiStartGold;
        }

        void Update()
        {
            buildTimer -= Time.deltaTime;

            if (buildTimer<=0)
            {
                gold += 30;
                int i;
                Tower largestThreat = towers[0];
                for (i = 1; i < towers.Length; i++)
                {
                    if (towers[i].GetThreat() > largestThreat.GetThreat())
                    {
                        largestThreat = towers[i];
                    }
                }

                Debug.Log("Threat: " + largestThreat.GetThreat() + ", Cost: " + largestThreat.GetUpgradeCost());

                if (largestThreat.GetThreat() > 0 && largestThreat.GetUpgradeCost() <= gold)
                {
                    if (largestThreat.level < 3)
                    {
                        gold -= largestThreat.GetUpgradeCost();
                        largestThreat.UpgradeTower();
                    }
                    largestThreat.ResetThreat();
                }

                buildTimer = waitForBuild;
            }

        }
    }
}

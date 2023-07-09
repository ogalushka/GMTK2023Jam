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

        private int gold = 100000;

        private void Start()
        {
            buildTimer = waitForBuild;
        }

        void Update()
        {
            if (buildTimer<=0)
            {
                int i;
                Tower largestThreat = towers[0];
                for (i = 1; i < towers.Length; i++)
                    if (towers[i].GetThreat() > largestThreat.GetThreat() ); 
                        largestThreat = towers[i];

                if (largestThreat.GetUpgradeCost() > gold)
                {
                    gold -= largestThreat.GetUpgradeCost();
                    largestThreat.UpgradeTower();
                }


            }

        }
    }
}

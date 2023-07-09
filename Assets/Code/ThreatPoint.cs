using Assets.Code.Data;
using System;
using UnityEngine;

namespace Assets.Code
{
    public class ThreatPoint : MonoBehaviour
    {
        [SerializeField] int threat = 1;
        [SerializeField] private Tower[] towers;

        void OnTriggerEnter2D(Collider2D collision)
        {
            for (int i=0; i<towers.Length; i++)
            {
                if (towers[i].level < 3)
                {
                    towers[i].RaiseThreat(threat);

                }
            }
        }

        public int GetThreatLevel()
        {
            return threat;
        }


    }
}
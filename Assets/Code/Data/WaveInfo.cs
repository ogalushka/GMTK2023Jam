using UnityEngine;

namespace Assets.Code.Data
{
    [CreateAssetMenu(fileName = "WaveInfo", menuName = "ScriptableObjects/WaveInfo", order = 1)]
    public class WaveInfo : ScriptableObject
    {
        public Unit unitPrefab;
        public int unitCount;
        public float spawnInterval;
        public int price;
    }
}

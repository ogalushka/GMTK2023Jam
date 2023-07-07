using UnityEngine;

namespace Assets.Code.Data
{
    public struct MoveResult
    {
        public Vector3 position;
        public Building buildingPassed;

        public MoveResult(Vector3 position, Building buildingPassed = null)
        {
            this.position = position;
            this.buildingPassed = buildingPassed;
        }
    }
}

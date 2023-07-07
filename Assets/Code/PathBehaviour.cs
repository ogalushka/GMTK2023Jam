using Assets.Code.Data;
using UnityEngine;

namespace Assets.Code
{
    public class PathBehaviour : MonoBehaviour
    {
        public PathNode[] nodes;

        public Transform First()
        {
            return nodes[0].transform;
        }

        public Transform Last()
        {
            return nodes[nodes.Length - 1].transform;
        }

        private void OnEnable()
        {
            RefreshDistances();
        }

        private void RefreshDistances()
        {
            for(var i = 0; i < nodes.Length; i++)
            {
                if (i == 0)
                {
                    nodes[i].distance = 0;
                    continue;
                }

                var currNode = nodes[i];
                var prevNode = nodes[i - 1];
                var currNodePosition = currNode.transform.position;
                var prevNodePosition = prevNode.transform.position;
                currNode.distance = prevNode.distance + (prevNodePosition - currNodePosition).magnitude;
            }
        }

        // Get position on a path at distance from start
        // TODO probably also rotation
        public Vector3 GetPositionAt(float distanceFromStart)
        {
            if (nodes.Length == 0)
            {
                Debug.LogError("Path can't be empty, add nodes");
                throw new System.Exception("Path can't be empty, add nodes");
            }

            var nextNode = nodes[0];
            var passedNode = nodes[0];

            foreach (var node in nodes)
            {
                if (node.distance >= distanceFromStart)
                {
                    nextNode = node;
                    break;
                }

                passedNode = node;
            }

            var segmentProgress = (distanceFromStart - passedNode.distance) / (nextNode.distance - passedNode.distance);
            return Vector3.Lerp(passedNode.transform.position, nextNode.transform.position, segmentProgress);
        }

        public Building GetPassedBuilding(float distanceFromStart)
        {
            foreach (var node in nodes)
            {
                if (node.distance >= distanceFromStart)
                {
                    break;
                }

                if (node.building != null && node.building.IsAlive())
                {
                    return node.building;
                }
            }

            return null;
        }

        //Debug view
        private void OnDrawGizmos()
        {
            if (nodes.Length > 0)
            {
                Gizmos.DrawSphere(nodes[0].transform.position, .1f);
            }

            for (var i = 1; i < nodes.Length; i++)
            {
                var curentPosition = nodes[i].transform.position;
                var prevPosition = nodes[i - 1].transform.position;
                Gizmos.DrawSphere(curentPosition, .1f);
                Gizmos.DrawLine(curentPosition, prevPosition);
            }
        }
    }
}

using Assets.Code.Data;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Collider2D))]
    public class LaneTargeter : MonoBehaviour
    {
        public PathBehaviour targetedPath;
        public PathEvent pathEvent;

        private Collider2D bounds;
        private bool mouseWasOver;

        private void Awake()
        {
            bounds = GetComponent<Collider2D>();
        }

        private void Update()
        {
            // OnMouseDown for some reason stops wokring when relaodin a scene, so i'm going manualy
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            var isOver = bounds.bounds.Contains(worldPos);

            if (Input.GetMouseButtonDown(0) && isOver)
            {
                pathEvent.Raise(targetedPath);
            }
        }
    }
}

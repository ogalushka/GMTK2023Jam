using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class Intro : MonoBehaviour
    {
        public Camera cam;

        public Vector3 startPosition;
        public Vector3 endPosition;
        public float duration;
        public float delay;
        public CameraController cameraController;

        private float currentDuration;

        private void Start()
        {
            cam.transform.position = startPosition;
            cameraController.enabled = false;
            StartCoroutine(AnimateCoroutine());
        }

        private IEnumerator AnimateCoroutine()
        {
            currentDuration = 0;
            while (currentDuration <= delay)
            {
                currentDuration += Time.deltaTime;
                yield return null;
            }

            currentDuration = 0;
            while (currentDuration <= duration)
            {
                cam.transform.position = Vector3.Lerp(startPosition, endPosition, currentDuration / duration);
                currentDuration += Time.deltaTime;
                yield return null;
            }
            cameraController.enabled = true;
        }
    }
}

using System.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class TextAnimator : MonoBehaviour
    {
        public float duration = 1;
        public Vector3 impulse;
        public Vector3 acceleration;

        private Vector3 speed;
        private Text text;
        private float timeLeft;

        private void Awake()
        {
            text = GetComponent<Text>();
            speed = impulse;
            timeLeft = duration;
        }

        public void SetAmount(int money)
        {
            text.text = $"{money}";
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime;
            speed += acceleration * Time.deltaTime;
            timeLeft -= Time.deltaTime;

            text.color = new Color(text.color.r, text.color.g, text.color.b, timeLeft / duration);

            if (timeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

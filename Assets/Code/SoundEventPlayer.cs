using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEventPlayer : MonoBehaviour
    {
        private AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}

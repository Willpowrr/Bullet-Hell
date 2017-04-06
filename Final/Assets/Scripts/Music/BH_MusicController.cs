using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_MusicController : MonoBehaviour {

        public AudioSource audioSource { get; protected set; }

        [SerializeField]
        protected AudioClip musicClip;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        void Start() {
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}

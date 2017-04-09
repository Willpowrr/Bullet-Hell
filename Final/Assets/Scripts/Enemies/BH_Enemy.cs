using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_Enemy : MonoBehaviour {

        public string id;

        public Rigidbody rigidBody { get; protected set; }
        public BH_EnemyController enemyController { get; set; }
        public AudioSource audioSource { get; protected set; }
        public BH_Enemy prefab { get; set; }

        [SerializeField]
        protected BezierCurve movementCurve;

        [SerializeField]
        protected float lifeTime;

        [SerializeField]
        protected int startHealth = 1;

        [SerializeField]
        protected AudioClip deathSoundClip;
        [SerializeField]
        protected float deathSoundVolume;
        [SerializeField]
        protected ParticleSystem deathParticlesPrefab;

        public bool active { get; set; }
        public int currentHealth { get; protected set; }
        public bool enteredScreen { get; set; }

        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            active = false;
        }
        
        void Start() {
        }
        
        void Update() {
        }

        public void Spawn() {
            active = true;
            currentHealth = startHealth;
            enteredScreen = false;
        }

        private void OnTriggerEnter(Collider other) {

            BH_Bullet bullet = other.gameObject.GetComponent<BH_Bullet>();
            if (bullet != null) {
                currentHealth--;
                if (currentHealth <= 0) {
                    Kill();
                }
            }

            BH_Ship ship = other.gameObject.GetComponent<BH_Ship>();
            if (ship != null) {
                Kill();
            }
        }

        protected void Kill() {
            ParticleSystem deathParticles = FastPoolManager.GetPool(deathParticlesPrefab, false).FastInstantiate<ParticleSystem>();
            deathParticles.transform.position = transform.position;
            audioSource.PlayOneShot(deathSoundClip, deathSoundVolume);
            enemyController.ReturnEnemy(this);
        }

        protected void OnFastDestroy() {

        }
    }
}

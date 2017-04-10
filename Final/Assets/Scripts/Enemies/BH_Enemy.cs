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
        public BH_GameplayController gameplayController { get; protected set; }

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
        [SerializeField]
        protected Vector3 spawnVelocity;
        [SerializeField]
        protected bool shootsBullets;
        [SerializeField]
        protected float shootVelocity;
        [SerializeField]
        protected float shootFrequency;

        public bool active { get; set; }
        public int currentHealth { get; protected set; }
        public bool enteredScreen { get; set; }
        protected float lastShotTime = 0.0f;

        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            active = false;
            gameplayController = FindObjectOfType<BH_GameplayController>();
        }
        
        public virtual void Start() {
        }

        public virtual void Update() {

            if (shootsBullets) {
                Vector3 shootPosition = transform.position;
                float currentTime = Time.time;
                if (currentTime - lastShotTime > shootFrequency) {
                    Vector3 shootVector = gameplayController.player.ship.transform.position - transform.position;
                    shootVector.Normalize();
                    shootVector *= shootVelocity;
                    enemyController.bulletController.Shoot(shootPosition, shootVector);
                    lastShotTime = currentTime;
                }
            }
        }

        public virtual void Spawn() {
            active = true;
            currentHealth = startHealth;
            enteredScreen = false;
            lastShotTime = Time.fixedTime;
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

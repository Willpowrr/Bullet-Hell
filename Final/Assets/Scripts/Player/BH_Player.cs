using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_Player : MonoBehaviour {
        
        public BH_CameraController cameraController { get; protected set; }
        public BH_Ship ship { get; protected set; }
        public BH_BulletController bulletController { get; protected set; }
        public BH_PlayerHealthUI playerHealthUI { get; protected set; }
        public AudioSource audioSource { get; protected set; }
        public BH_InputController inputController { get; protected set; }
        public BH_GameplayController gameplayController { get; protected set; }
        
        public int startHealth = 5;
        public AudioClip shootSoundClip;
        public float shootSoundVolume;

        [SerializeField]
        protected Vector3 shootOffset;
        [SerializeField]
        protected ParticleSystem shootParticlesPrefab;
        [SerializeField]
        protected ParticleSystem deathParticlesPrefab;
        [SerializeField]
        protected ParticleSystem deadParticlesPrefab;

        public ParticleSystem deadParticles { get; protected set; }

        public int currentHealth { get; protected set; }
        public bool alive { get { return currentHealth > 0; } }
        public bool inputActive { get; set; }
        public Vector3 shipPosition { get { return ship.transform.position; } set { ship.transform.position = value; } }

        public void Awake() {

            cameraController = FindObjectOfType<BH_CameraController>();
            ship = GetComponentInChildren<BH_Ship>();
            bulletController = GetComponentInChildren<BH_BulletController>();
            playerHealthUI = FindObjectOfType<BH_PlayerHealthUI>();
            audioSource = GetComponentInChildren<AudioSource>();
            inputController = GetComponent<BH_InputController>();
            gameplayController = FindObjectOfType<BH_GameplayController>();
        }

        // Use this for initialization//
        void Start() {

            Reset();
            playerHealthUI.Initialize(startHealth);
        }

        public void Reset() {
            inputActive = false;
            currentHealth = startHealth;
            playerHealthUI.UpdateHealth(currentHealth);
            ship.gameObject.SetActive(true);
            if (deadParticles != null) {
                FastPoolManager.GetPool(deadParticlesPrefab, false).FastDestroy(deadParticles.gameObject);
            }
        }

        // Update is called once per frame
        void Update() {

            if (alive && inputActive) {
                if (Input.GetKey(KeyCode.Space)) {
                    Shoot();
                }

                Vector3 direction = Vector3.zero;
                if (inputController.right) {
                    direction.x = 1.0f;
                    ship.horizontalMovementState = BH_Ship.HorizontalMovementState.Forward;
                }
                else if (inputController.left) {
                    direction.x = -1.0f;
                    ship.horizontalMovementState = BH_Ship.HorizontalMovementState.Backward;
                }
                else {
                    ship.horizontalMovementState = BH_Ship.HorizontalMovementState.Idle;
                }

                if (inputController.up) {
                    direction.y = 1.0f;
                    ship.verticalMovementState = BH_Ship.VerticalMovementState.Up;
                }
                else if (inputController.down) {
                    direction.y = -1.0f;
                    ship.verticalMovementState = BH_Ship.VerticalMovementState.Down;
                }
                else {
                    ship.verticalMovementState = BH_Ship.VerticalMovementState.Idle;
                }

                Vector3 pos = ship.transform.position + direction * ship.moveSpeed * Time.deltaTime;
                pos.x = Mathf.Clamp(pos.x, -gameplayController.screenBounds.x, gameplayController.screenBounds.x);
                pos.y = Mathf.Clamp(pos.y, -gameplayController.screenBounds.y, gameplayController.screenBounds.y);
                pos.z = 0.0f;

                ship.rigidBody.MovePosition(pos);
            }
        }

        protected void Shoot() {

            Vector3 shootPosition = ship.transform.position + shootOffset;
            bulletController.Shoot(shootPosition);
            FastPoolManager.GetPool(shootParticlesPrefab, false).FastInstantiate<ParticleSystem>().transform.position = shootPosition;
        }

        public void PlayShootSound() {
            audioSource.PlayOneShot(shootSoundClip, shootSoundVolume);
        }

        public void Damage(int p_damage) {
            if (alive) {
                currentHealth -= p_damage;
                currentHealth = Mathf.Max(0, currentHealth);
                if (currentHealth == 0) {
                    Kill();
                }
            }
            playerHealthUI.UpdateHealth(currentHealth);
        }

        protected void Kill() {
            FastPoolManager.GetPool(deathParticlesPrefab, false).FastInstantiate<ParticleSystem>().transform.position = ship.transform.position;
            deadParticles = FastPoolManager.GetPool(deadParticlesPrefab, false).FastInstantiate<ParticleSystem>();
            deadParticles.transform.position = ship.transform.position;
            ship.gameObject.SetActive(false);
            gameplayController.EndGame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_BulletController : MonoBehaviour {

        public BH_CameraController cameraController { get; protected set; }
        public BH_Player player { get; protected set; }

        public int bulletCacheSize = 30;
        public BH_Bullet bulletPrefab;
        public Vector3 inactivePosition;
        public float bulletVelocity = 1.0f;
        public float shootFrequency = 0.1f;
        public Transform bulletsTransform;

        public int explosionParticleCacheSize = 10;
        public ParticleSystem explosionParticlePrefab;
        public Transform explosionParticlesTransform;

        protected float lastShotTime = 0.0f;

        public Queue<BH_Bullet> bulletCache { get; protected set; }
        public List<BH_Bullet> activeBullets { get; protected set; }
        protected List<BH_Bullet> bulletRemoveList { get; set; }

        public Queue<ParticleSystem> explosionParticlesCache { get; protected set; }
        public List<ParticleSystem> activeExplosionParticles { get; protected set; }
        protected List<ParticleSystem> explosionParticleRemoveList { get; set; }

        private void Awake() {
            CreateBulletCache();
            CreateExplosionParticlesCache();
            cameraController = FindObjectOfType<BH_CameraController>();
            player = GetComponentInParent<BH_Player>();
        }

        protected void CreateBulletCache() {
            bulletCache = new Queue<BH_Bullet>();
            activeBullets = new List<BH_Bullet>();
            bulletRemoveList = new List<BH_Bullet>();
            for (int i = 0; i < bulletCacheSize; ++i) {
                BH_Bullet bullet = Instantiate(bulletPrefab, bulletsTransform);
                bullet.transform.position = inactivePosition;
                bullet.bulletController = this;
                bulletCache.Enqueue(bullet);
            }
        }

        protected void CreateExplosionParticlesCache() {
            explosionParticlesCache = new Queue<ParticleSystem>();
            activeExplosionParticles = new List<ParticleSystem>();
            explosionParticleRemoveList = new List<ParticleSystem>();
            for (int i = 0; i < explosionParticleCacheSize; ++i) {
                ParticleSystem explosionParticleSystem = Instantiate(explosionParticlePrefab, explosionParticlesTransform);
                explosionParticleSystem.transform.position = inactivePosition;
                ParticleSystem.EmissionModule emission = explosionParticleSystem.emission;
                emission.enabled = false;
                explosionParticlesCache.Enqueue(explosionParticleSystem);
            }
        }

        protected BH_Bullet GetBullet() {
            BH_Bullet bullet = bulletCache.Dequeue();
            activeBullets.Add(bullet);
            return bullet;
        }

        public void ReturnBullet(BH_Bullet p_bullet) {
            activeBullets.Remove(p_bullet);
            bulletCache.Enqueue(p_bullet);
            p_bullet.transform.position = inactivePosition;
            p_bullet.transform.position = inactivePosition;
            p_bullet.rigidBody.velocity = Vector3.zero;
        }

        protected ParticleSystem GetExplosionParticleSystem() {
            ParticleSystem particleSystem = explosionParticlesCache.Dequeue();
            activeExplosionParticles.Add(particleSystem);
            return particleSystem;
        }

        public void ReturnExplosionParticleSystem(ParticleSystem p_particleSystem) {
            activeExplosionParticles.Remove(p_particleSystem);
            explosionParticlesCache.Enqueue(p_particleSystem);
            p_particleSystem.transform.position = inactivePosition;
            p_particleSystem.transform.position = inactivePosition;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

            CheckActiveBulletsOffScreen();
            CheckExplosionParticlesFinished();
        }

        protected void CheckActiveBulletsOffScreen() {

            bulletRemoveList.Clear();
            foreach (BH_Bullet bullet in activeBullets) {
                Vector3 screenPos = cameraController.mainCamera.WorldToViewportPoint(bullet.transform.position);
                if (screenPos.x < -0.1f || screenPos.x > 1.1f || screenPos.y < -0.1f || screenPos.y > 1.1f) {
                    bulletRemoveList.Add(bullet);
                }
            }

            foreach (BH_Bullet bullet in bulletRemoveList) {
                ReturnBullet(bullet);
            }
        }

        protected void CheckExplosionParticlesFinished() {

            explosionParticleRemoveList.Clear();
            foreach (ParticleSystem particleSystem in activeExplosionParticles) {
                if (particleSystem.isStopped) {
                    explosionParticleRemoveList.Add(particleSystem);
                }
            }

            foreach (ParticleSystem particleSystem in explosionParticleRemoveList) {
                ReturnExplosionParticleSystem(particleSystem);
            }
        }

        public void Shoot() {

            float currentTime = Time.time;
            if (currentTime - lastShotTime > shootFrequency) {
                BH_Bullet bullet = GetBullet();
                bullet.transform.position = player.ship.transform.position;
                bullet.rigidBody.velocity = new Vector3(bulletVelocity, 0.0f, 0.0f);
                if (player != null) {
                    player.PlayShootSound();
                }
                lastShotTime = currentTime;
            }
        }

        public void CreateExplosionParticles(Vector3 p_position) {
            ParticleSystem particleSystem = GetExplosionParticleSystem();
            particleSystem.transform.position = p_position;
            ParticleSystem.EmissionModule emission = particleSystem.emission;
            emission.enabled = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_BulletController : MonoBehaviour {

        public BH_CameraController cameraController { get; protected set; }
        public BH_Player player { get; protected set; }
        
        public BH_Bullet bulletPrefab;
        public float bulletVelocity = 1.0f;
        public float shootFrequency = 0.1f;

        protected float lastShotTime = 0.0f;

        protected List<BH_Bullet> activeBullets = new List<BH_Bullet>();
        protected List<BH_Bullet> bulletRemoveList = new List<BH_Bullet>();

        [SerializeField]
        protected Transform bulletsContainer;

        private void Awake() {
            cameraController = FindObjectOfType<BH_CameraController>();
            player = GetComponentInParent<BH_Player>();
        }

        protected BH_Bullet GetBullet() {
            BH_Bullet bullet = FastPoolManager.GetPool(bulletPrefab, false).FastInstantiate<BH_Bullet>(bulletsContainer.transform);
            bullet.bulletController = this;
            activeBullets.Add(bullet);
            return bullet;
        }

        public void ReturnBullet(BH_Bullet p_bullet) {
            activeBullets.Remove(p_bullet);
            p_bullet.rigidBody.velocity = Vector3.zero;
            p_bullet.transform.SetParent(FastPoolManager.Instance.transform);
            p_bullet.bulletController = null;
            FastPool pool = FastPoolManager.GetPool(bulletPrefab, false);
            pool.FastDestroy(p_bullet.gameObject);
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

            CheckActiveBulletsOffScreen();
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

        public void Shoot(Vector3 p_position) {

            float currentTime = Time.time;
            if (currentTime - lastShotTime > shootFrequency) {
                BH_Bullet bullet = GetBullet();
                bullet.transform.position = p_position;
                bullet.rigidBody.velocity = new Vector3(bulletVelocity, 0.0f, 0.0f);
                if (player != null) {
                    player.PlayShootSound();
                }
                lastShotTime = currentTime;
            }
        }
    }
}
﻿using System.Collections;
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

        protected float lastShotTime = 0.0f;

		public Queue<BH_Bullet> bulletCache {get; protected set;}
        public List<BH_Bullet> activeBullets { get; protected set; }

        protected List<BH_Bullet> removeList { get; set; }

		private void Awake () {
			CreateBulletCache();
            cameraController = FindObjectOfType<BH_CameraController>();
            player = GetComponentInParent<BH_Player>();
		}

		protected void CreateBulletCache(){
			bulletCache = new Queue<BH_Bullet>();
            activeBullets = new List<BH_Bullet>();
            removeList = new List<BH_Bullet>();
			for (int i = 0; i <bulletCacheSize; ++i) {
				BH_Bullet bullet = Instantiate(bulletPrefab);
				bullet.transform.SetParent(transform);
                bullet.transform.position = inactivePosition;
                bullet.bulletController = this;
				bulletCache.Enqueue(bullet);
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

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

            CheckActiveBulletsOffScreen();
		}

        protected void CheckActiveBulletsOffScreen() {

            removeList.Clear();
            foreach (BH_Bullet bullet in activeBullets) {
                Vector3 screenPos = cameraController.mainCamera.WorldToViewportPoint(bullet.transform.position);
                if (screenPos.x < -0.1f || screenPos.x > 1.1f || screenPos.y < -0.1f || screenPos.y > 1.1f) {
                    removeList.Add(bullet);
                }
            }

            foreach (BH_Bullet bullet in removeList) {
                ReturnBullet(bullet);
            }
        }

        public void Shoot() {

            float currentTime = Time.time;
            if (currentTime - lastShotTime > shootFrequency) {
                BH_Bullet bullet = GetBullet();
                bullet.transform.position = player.ship.transform.position;
                bullet.rigidBody.velocity = new Vector3(bulletVelocity, 0.0f, 0.0f);
                lastShotTime = currentTime;
            }
        }
	}
}
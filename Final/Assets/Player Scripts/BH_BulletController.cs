using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
	public class BH_BulletController : MonoBehaviour {
        
        public BH_Player player { get; protected set; }

		public int bulletCacheSize = 30;
		public BH_Bullet bulletPrefab;
		public Vector3 inactivePosition;

		protected Queue<BH_Bullet> bulletCache {get;set;}

		private void Awake () {
			CreateBulletCache();
            player = GetComponentInParent<BH_Player>();
		}

		protected void CreateBulletCache(){
			bulletCache = new Queue<BH_Bullet>();
			for (int i = 0; i <bulletCacheSize; ++i) {
				BH_Bullet bullet = Instantiate(bulletPrefab);
				bullet.transform.SetParent(transform);
				bullet.transform.position=inactivePosition;
				bulletCache.Enqueue(bullet);
			}
		}

        protected BH_Bullet GetBullet() {
            return bulletCache.Dequeue();
        }

        protected void ReturnBullet(BH_Bullet p_bullet) {
            bulletCache.Enqueue(p_bullet);
            p_bullet.movementController.position = inactivePosition;
            p_bullet.movementController.velocity = Vector3.zero;
        }

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}

        public void Shoot() {
            BH_Bullet bullet = GetBullet();
            bullet.movementController.position = player.ship.movementController.position;
            bullet.movementController.velocity = new Vector3(0.1f, 0.0f, 0.0f);
        }
	}
}
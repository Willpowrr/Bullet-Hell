using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
	public class BH_BulletController : MonoBehaviour {

		public int bulletCacheSize = 30;
		public BH_Bullet bulletPrefab;
		public Vector3 inactivePosition;
		protected Queue<BH_Bullet> bulletCache {get;set;}

		private void Awake () {
			CreateBulletCache();
		}

		protected void CreateBulletCache(){
			bulletCache=new Queue<BH_Bullet>();
			for (int i = 0; i <bulletCacheSize; ++i) {
				BH_Bullet bullet = Instantiate(bulletPrefab);
				bullet.transform.SetParent(transform);
				bullet.transform.position=inactivePosition;
				bulletCache.Enqueue(bullet);
			}
		}


		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}
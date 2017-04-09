using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletHell;

namespace BulletHell {

    public class BH_Bullet : MonoBehaviour {
        
        public Rigidbody rigidBody { get; protected set; }
        public BH_BulletController bulletController { get; set; }

        [SerializeField]
        protected ParticleSystem hitParticlePrefab;

        public void Awake() {
            rigidBody = GetComponent<Rigidbody>();
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter(Collider other) {
            Kill();
        }

        protected void Kill() {
            FastPoolManager.GetPool(hitParticlePrefab, false).FastInstantiate<ParticleSystem>().transform.position = transform.position;
            bulletController.ReturnBullet(this);
        }

        protected void OnFastDestroy() {

        }
    }
}

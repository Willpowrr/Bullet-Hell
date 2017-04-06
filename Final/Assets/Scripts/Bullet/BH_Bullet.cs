using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletHell;

namespace BulletHell {

    public class BH_Bullet : MonoBehaviour {
        
        public Rigidbody rigidBody { get; protected set; }
        public BH_BulletController bulletController { get; set; }

        public void Awake() {
            rigidBody = GetComponent<Rigidbody>();
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnCollisionEnter(Collision collision) {
            bulletController.ReturnBullet(this);
        }
    }
}

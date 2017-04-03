using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_MovementController : MonoBehaviour {

		public Vector3 position;
		public Vector3 velocity;

        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
            position += velocity;
        }

        public void LateUpdate() {
            transform.position = position;
        }

        public void SetVelocity(Vector3 p_velocity) {
            velocity = p_velocity;
        }
    }
}

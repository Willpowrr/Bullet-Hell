using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_Bouncer : BH_Enemy {

        public override void Start() {
            base.Start();
        }

        public override void Update() {
            base.Update();
            
            if (enteredScreen) {
                Vector3 velocity = rigidBody.velocity;
                if (velocity.y > 0) {
                    if (rigidBody.transform.position.y > gameplayController.screenBounds.y) {
                        velocity.y *= -1.0f;
                        rigidBody.velocity = velocity;
                    }
                }
                else {
                    if (rigidBody.transform.position.y < -gameplayController.screenBounds.y) {
                        velocity.y *= -1.0f;
                        rigidBody.velocity = velocity;
                    }
                }
            }
        }

        public override void Spawn() {
            base.Spawn();
            Vector3 velocity = spawnVelocity;
            if (Random.Range(-1.0f, 1.0f) > 0) {
                velocity.y *= -1.0f;
            }
            rigidBody.velocity = velocity;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_DumbassEnemy : BH_Enemy {

        public override void Start() {
            base.Start();
        }

        public override void Update() {
            base.Update();
        }

        public override void Spawn() {
            base.Spawn();
            rigidBody.velocity = spawnVelocity;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_Enemy : MonoBehaviour {
        
        public string enemyID;

        [SerializeField]
        protected BezierCurve movementCurve;

        [SerializeField]
        protected float lifeTime;

        public BH_MovementController movementController { get; protected set; }

        public bool active { get; set; }
        public bool livedItsLife { get { return (Time.time - birthTime) > lifeTime; } }

        protected float birthTime = 0.0f;

        private void Awake() {
            movementController = GetComponent<BH_MovementController>();
            active = false;
        }
        
        void Start() {
        }
        
        void Update() {
        }

        public void Spawn() {
            birthTime = Time.time;
            active = true;
        }
    }
}

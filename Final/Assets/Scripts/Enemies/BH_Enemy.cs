using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_Enemy : MonoBehaviour {
        
        public string enemyID;

        public Rigidbody rigidBody { get; protected set; }
        public BH_EnemyController enemyController { get; set; }

        [SerializeField]
        protected BezierCurve movementCurve;

        [SerializeField]
        protected float lifeTime;

        [SerializeField]
        protected int startHealth = 1;

        public bool active { get; set; }
        public int currentHealth { get; protected set; }
        public bool enteredScreen { get; set; }

        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
            active = false;
        }
        
        void Start() {
        }
        
        void Update() {
        }

        public void Spawn() {
            active = true;
            currentHealth = startHealth;
            enteredScreen = false;
        }

        private void OnTriggerEnter(Collider other) {

            BH_Bullet bullet = other.gameObject.GetComponent<BH_Bullet>();
            if (bullet != null) {
                currentHealth--;
                if (currentHealth <= 0) {
                    enemyController.ReturnEnemy(this);
                }
            }

            BH_Ship ship = other.gameObject.GetComponent<BH_Ship>();
            if (ship != null) {
                enemyController.ReturnEnemy(this);
            }
        }
    }
}

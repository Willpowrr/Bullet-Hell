using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_Ship : MonoBehaviour {

        public BH_Player player { get; protected set; }
        public BH_MovementController movementController { get; protected set; }
        public BH_InputController inputController { get; protected set; }

        public float moveSpeed;

        public void Awake() {

            player = GetComponentInParent<BH_Player>();
            movementController = GetComponent<BH_MovementController>();
            inputController = GetComponent<BH_InputController>();

            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

            Vector3 newVelocity = Vector3.zero;
            if (inputController.right) {
                newVelocity.x += moveSpeed;
            }
            else if (inputController.left) {
                newVelocity.x -= moveSpeed;
            }

            if (inputController.up) {
                newVelocity.y += moveSpeed;
            }
            else if (inputController.down) {
                newVelocity.y -= moveSpeed;
            }

            movementController.SetVelocity(newVelocity);
        }
    }
}

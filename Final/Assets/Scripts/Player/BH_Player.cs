using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_Player : MonoBehaviour {
        
        public BH_CameraController cameraController { get; protected set; }
        public BH_Ship ship { get; protected set; }
        public BH_BulletController bulletController { get; protected set; }

        public Vector3 startPosition;
        public Vector3 minPosition;
        public Vector3 maxPosition;

        public void Awake() {

            cameraController = FindObjectOfType<BH_CameraController>();
            ship = GetComponentInChildren<BH_Ship>();
            bulletController = GetComponentInChildren<BH_BulletController>();
        }

        // Use this for initialization//
        void Start() {

            ship.movementController.position = startPosition;
        }

        // Update is called once per frame
        void Update() {
            ship.movementController.position.x = Mathf.Clamp(ship.movementController.position.x, minPosition.x, maxPosition.x);
            ship.movementController.position.y = Mathf.Clamp(ship.movementController.position.y, minPosition.y, maxPosition.y);

            if (Input.GetKey(KeyCode.Space)) {
                bulletController.Shoot();
            }
        }
    }
}

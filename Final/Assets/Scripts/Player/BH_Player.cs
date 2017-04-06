using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_Player : MonoBehaviour {
        
        public BH_CameraController cameraController { get; protected set; }
        public BH_Ship ship { get; protected set; }
        public BH_BulletController bulletController { get; protected set; }
        public BH_PlayerHealthUI playerHealthUI { get; protected set; }

        public Vector3 startPosition;
        public Vector3 minPosition;
        public Vector3 maxPosition;
        public int startHealth = 5;

        public int currentHealth { get; protected set; }

        public void Awake() {

            cameraController = FindObjectOfType<BH_CameraController>();
            ship = GetComponentInChildren<BH_Ship>();
            bulletController = GetComponentInChildren<BH_BulletController>();
            playerHealthUI = FindObjectOfType<BH_PlayerHealthUI>();
        }

        // Use this for initialization//
        void Start() {

            ship.transform.position = startPosition;
            currentHealth = startHealth;
            playerHealthUI.Initialize(startHealth);
        }

        // Update is called once per frame
        void Update() {
            Vector3 position = ship.transform.position;
            position.x = Mathf.Clamp(ship.transform.position.x, minPosition.x, maxPosition.x);
            position.y = Mathf.Clamp(ship.transform.position.y, minPosition.y, maxPosition.y);
            position.z = 0.0f;
            ship.transform.position = position;

            if (Input.GetKey(KeyCode.Space)) {
                bulletController.Shoot();
            }
        }

        public void Damage(int p_damage) {
            currentHealth -= p_damage;
            currentHealth = Mathf.Max(0, currentHealth);
            playerHealthUI.UpdateHealth(currentHealth);
        }
    }
}

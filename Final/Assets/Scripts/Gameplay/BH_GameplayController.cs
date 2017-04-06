using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_GameplayController : MonoBehaviour {

        public BH_Player player { get; protected set; }
        public BH_CameraController cameraController { get; protected set; }
        public BH_EnemyController enemyController { get; protected set; }

        private void Awake() {
            player = GetComponentInChildren<BH_Player>();
            cameraController = GetComponentInChildren<BH_CameraController>();
            enemyController = GetComponentInChildren<BH_EnemyController>();

            //Application.targetFrameRate = 60;
            //QualitySettings.vSyncCount = 0;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public bool IsOnScreen(Transform p_transform) {
            Vector3 screenPos = cameraController.mainCamera.WorldToViewportPoint(p_transform.position);
            return !(screenPos.x < -0.1f || screenPos.x > 1.1f || screenPos.y < -0.1f || screenPos.y > 1.1f);
        }
    }
}

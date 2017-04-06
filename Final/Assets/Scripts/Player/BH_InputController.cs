using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_InputController : MonoBehaviour {
        

        public bool up { get; protected set; }
        public bool right { get; protected set; }
        public bool down { get; protected set; }
        public bool left { get; protected set; }
		public bool fire1 { get; protected set; }

        private void Awake() {
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            up = Input.GetButton("Up");
            down = Input.GetButton("Down");
            right = Input.GetButton("Right");
            left = Input.GetButton("Left");
			fire1 = Input.GetButton ("Fire1");

            if (up && down) {
                up = false;
                down = false;
            }
            if (left && right) {
                left = false;
                right = false;
            }
        }
    }
}

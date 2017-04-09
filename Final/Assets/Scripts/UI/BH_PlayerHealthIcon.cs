using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletHell {
    public class BH_PlayerHealthIcon : MonoBehaviour {

        public Image image { get; protected set; }

        [SerializeField]
        protected Color activeColor;
        [SerializeField]
        protected Color inactiveColor;

        private void Awake() {
            image = GetComponent<Image>();
        }

        void Start() {
            image.color = activeColor;
        }

        public void Activate() {
            image.color = activeColor;
        }

        public void Deactivate() {
            image.color = inactiveColor;
        }
    }
}

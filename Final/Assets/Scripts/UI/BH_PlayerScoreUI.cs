using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BulletHell {
    public class BH_PlayerScoreUI : MonoBehaviour {

        [SerializeField]
        protected TextMeshProUGUI valueText;

        void Start() {

        }

        public void SetScore(int p_score) {
            valueText.SetText("{0}", p_score);
        }
    }
}

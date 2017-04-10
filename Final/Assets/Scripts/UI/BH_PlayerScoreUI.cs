using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BulletHell {
    public class BH_PlayerScoreUI : MonoBehaviour {

        [SerializeField]
        protected TextMeshProUGUI valueText;
        [SerializeField]
        protected TextMeshProUGUI highValueText;

        void Start() {

        }

        public void SetScore(int p_score, int p_highScore) {
            valueText.SetText("{0}", p_score);
            highValueText.SetText("{0}", p_highScore);
        }
    }
}

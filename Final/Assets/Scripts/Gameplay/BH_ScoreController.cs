using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_ScoreController : MonoBehaviour {

        public BH_PlayerScoreUI playerScoreUI { get; protected set; }

        [SerializeField]
        protected float timeScale = 1.0f;

        public int score { get; protected set; }

        void Awake() {
            playerScoreUI = FindObjectOfType<BH_PlayerScoreUI>();
        }

        void Start() {
            score = 0;
            playerScoreUI.SetScore(score);
        }

        void Update() {
            score += (int)(Time.deltaTime * timeScale);
            playerScoreUI.SetScore(score);
        }
    }
}

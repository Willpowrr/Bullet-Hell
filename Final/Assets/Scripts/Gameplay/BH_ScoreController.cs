using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_ScoreController : MonoBehaviour {

        public BH_PlayerScoreUI playerScoreUI { get; protected set; }
        public BH_GameplayController gameplayController { get; protected set; }

        [SerializeField]
        protected float timeScale = 1.0f;

        public int score { get; protected set; }
        public int highScore { get; protected set; }

        void Awake() {
            gameplayController = GetComponent<BH_GameplayController>();
            playerScoreUI = FindObjectOfType<BH_PlayerScoreUI>();
        }

        void Start() {
            Reset();
        }

        void Update() {
            if (gameplayController.player.alive && gameplayController.gameActive) {
                score += (int)(Time.deltaTime * timeScale);
                if (score > highScore) {
                    highScore = score;
                }
                playerScoreUI.SetScore(score, highScore);
            }
        }

        public void Reset() {
            score = 0;
            highScore = PlayerPrefs.GetInt("BH_HighScore", 0);
            playerScoreUI.SetScore(score, highScore);
        }

        public void SaveScore() {
            if (score >= highScore) {
                PlayerPrefs.SetInt("BH_HighScore", score);
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_Engine : MonoBehaviour {

        public BH_GameplayController gameplayController { get; protected set; }
        public PlayMakerFSM playmakerFSM { get; protected set; }

        [SerializeField]
        protected int framerate = 60;

        [SerializeField]
        protected bool vSync = false;

        public void Awake() {
            gameplayController = GetComponent<BH_GameplayController>();
            playmakerFSM = GetComponent<PlayMakerFSM>();

            Application.targetFrameRate = framerate;
            QualitySettings.vSyncCount = vSync ? 1 : 0;
        }

        void Start() {
        }
        
        void Update() {
        }

        public void EndGame() {
            playmakerFSM.SendEvent("END GAME");
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletHell;

namespace BulletHell {

    public class BH_Bullet : MonoBehaviour {

        public BH_MovementController movementController { get; protected set; }
        public BH_BulletController bulletController { get; set; }

        public void Awake() {
            movementController = GetComponent<BH_MovementController>();
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}

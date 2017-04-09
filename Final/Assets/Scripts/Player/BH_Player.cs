﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_Player : MonoBehaviour {
        
        public BH_CameraController cameraController { get; protected set; }
        public BH_Ship ship { get; protected set; }
        public BH_BulletController bulletController { get; protected set; }
        public BH_PlayerHealthUI playerHealthUI { get; protected set; }
        public AudioSource audioSource { get; protected set; }
        public BH_InputController inputController { get; protected set; }

        public Vector3 startPosition;
        public Vector3 minPosition;
        public Vector3 maxPosition;
        public int startHealth = 5;
        public AudioClip shootSoundClip;
        public float shootSoundVolume;

        public int currentHealth { get; protected set; }

        public void Awake() {

            cameraController = FindObjectOfType<BH_CameraController>();
            ship = GetComponentInChildren<BH_Ship>();
            bulletController = GetComponentInChildren<BH_BulletController>();
            playerHealthUI = FindObjectOfType<BH_PlayerHealthUI>();
            audioSource = GetComponentInChildren<AudioSource>();
            inputController = GetComponent<BH_InputController>();
        }

        // Use this for initialization//
        void Start() {

            ship.transform.position = startPosition;
            currentHealth = startHealth;
            playerHealthUI.Initialize(startHealth);
        }

        // Update is called once per frame
        void Update() {

            if (Input.GetKey(KeyCode.Space)) {
                Shoot();
            }
            
            Vector3 direction = Vector3.zero;
            if (inputController.right) {
                direction.x = 1.0f;
                ship.horizontalMovementState = BH_Ship.HorizontalMovementState.Forward;
            }
            else if (inputController.left) {
                direction.x = -1.0f;
                ship.horizontalMovementState = BH_Ship.HorizontalMovementState.Backward;
            }
            else {
                ship.horizontalMovementState = BH_Ship.HorizontalMovementState.Idle;
            }

            if (inputController.up) {
                direction.y = 1.0f;
                ship.verticalMovementState = BH_Ship.VerticalMovementState.Up;
            }
            else if (inputController.down) {
                direction.y = -1.0f;
                ship.verticalMovementState = BH_Ship.VerticalMovementState.Down;
            }
            else {
                ship.verticalMovementState = BH_Ship.VerticalMovementState.Idle;
            }

            Vector3 pos = ship.transform.position + direction * ship.moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);
            pos.z = 0.0f;

            ship.rigidBody.MovePosition(pos);
        }

        protected void Shoot() {
            bulletController.Shoot();
        }

        public void PlayShootSound() {
            audioSource.PlayOneShot(shootSoundClip, shootSoundVolume);
        }

        public void Damage(int p_damage) {
            currentHealth -= p_damage;
            currentHealth = Mathf.Max(0, currentHealth);
            playerHealthUI.UpdateHealth(currentHealth);
        }
    }
}
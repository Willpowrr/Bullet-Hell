using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {

    public class BH_Ship : MonoBehaviour {

        public enum HorizontalMovementState : int {
            Idle = 0,
            Forward,
            Backward
        }

        public enum VerticalMovementState : int {
            Idle = 0,
            Up,
            Down
        }

        public BH_Player player { get; protected set; }
        public BH_MovementController movementController { get; protected set; }
        public BH_InputController inputController { get; protected set; }

        [SerializeField]
        protected Animator shipAnimator;

        [SerializeField]
        protected List<ParticleSystem> idleParticles = new List<ParticleSystem>();

        [SerializeField]
        protected List<ParticleSystem> forwardParticles = new List<ParticleSystem>();

        [SerializeField]
        protected List<ParticleSystem> backwardParticles = new List<ParticleSystem>();

        public float moveSpeed;

        protected HorizontalMovementState _horizontalMovementState = HorizontalMovementState.Idle;
        public HorizontalMovementState horizontalMovementState {
            get { return _horizontalMovementState; }
            set {
                if (_horizontalMovementState == value) {
                    return;
                }
                switch (_horizontalMovementState) {
                    case HorizontalMovementState.Idle:
                        SetIdleParticlesEnabled(false);
                        break;

                    case HorizontalMovementState.Forward:
                        SetForwardParticlesEnabled(false);
                        break;

                    case HorizontalMovementState.Backward:
                        SetBackwardParticlesEnabled(false);
                        break;
                }
                _horizontalMovementState = value;
                switch (_horizontalMovementState) {
                    case HorizontalMovementState.Idle:
                        SetIdleParticlesEnabled(true);
                        break;

                    case HorizontalMovementState.Forward:
                        SetForwardParticlesEnabled(true);
                        break;

                    case HorizontalMovementState.Backward:
                        SetBackwardParticlesEnabled(true);
                        break;
                }
            }
        }

        protected VerticalMovementState _verticalMovementState = VerticalMovementState.Idle;
        public VerticalMovementState verticalMovementState {
            get { return _verticalMovementState; }
            set {
                if (_verticalMovementState == value) {
                    return;
                }
                _verticalMovementState = value;
                switch (_verticalMovementState) {
                    case VerticalMovementState.Idle:
                        shipAnimator.CrossFade("Idle", 0.1f);
                        break;

                    case VerticalMovementState.Up:
                        shipAnimator.CrossFade("Up", 0.1f);
                        break;

                    case VerticalMovementState.Down:
                        shipAnimator.CrossFade("Down", 0.1f);
                        break;
                }

            }
        }

        public void Awake() {

            player = GetComponentInParent<BH_Player>();
            movementController = GetComponent<BH_MovementController>();
            inputController = GetComponent<BH_InputController>();

            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        // Use this for initialization
        void Start() {

            DisableAllParticles();
            SetIdleParticlesEnabled(true);
        }

        // Update is called once per frame
        void Update() {

            Vector3 newVelocity = Vector3.zero;
            if (inputController.right) {
                newVelocity.x += moveSpeed;
                horizontalMovementState = HorizontalMovementState.Forward;
            }
            else if (inputController.left) {
                newVelocity.x -= moveSpeed;
                horizontalMovementState = HorizontalMovementState.Backward;
            }
            else {
                horizontalMovementState = HorizontalMovementState.Idle;
            }

            if (inputController.up) {
                newVelocity.y += moveSpeed;
                verticalMovementState = VerticalMovementState.Up;
            }
            else if (inputController.down) {
                newVelocity.y -= moveSpeed;
                verticalMovementState = VerticalMovementState.Down;
            }
            else {
                verticalMovementState = VerticalMovementState.Idle;
            }

            movementController.SetVelocity(newVelocity);
        }

        protected void SetParticleSystemsEnabled(List<ParticleSystem> p_list, bool p_enabled) {
            foreach (ParticleSystem particleSystem in p_list) {
                ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
                emissionModule.enabled = p_enabled;
            }
        }

        protected void DisableAllParticles() {
            SetIdleParticlesEnabled(false);
            SetForwardParticlesEnabled(false);
            SetBackwardParticlesEnabled(false);
        }

        public void SetIdleParticlesEnabled(bool p_enabled) {
            SetParticleSystemsEnabled(idleParticles, p_enabled);
        }

        public void SetForwardParticlesEnabled(bool p_enabled) {
            SetParticleSystemsEnabled(forwardParticles, p_enabled);
        }

        public void SetBackwardParticlesEnabled(bool p_enabled) {
            SetParticleSystemsEnabled(backwardParticles, p_enabled);
        }
    }
}

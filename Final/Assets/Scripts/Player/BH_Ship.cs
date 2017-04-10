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

        public Rigidbody rigidBody { get; protected set; }
        public BH_Player player { get; protected set; }
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
                        shipAnimator.CrossFade("Idle", 0.25f);
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

            rigidBody = GetComponent<Rigidbody>();
            player = GetComponentInParent<BH_Player>();
            inputController = GetComponent<BH_InputController>();
        }

        // Use this for initialization
        void Start() {

            DisableAllParticles();
            SetIdleParticlesEnabled(true);
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

        private void OnTriggerEnter(Collider other) {

            BH_Enemy enemy = other.gameObject.GetComponent<BH_Enemy>();
            if (enemy != null) {
                player.Damage(1);
            }

            BH_Bullet bullet = other.gameObject.GetComponent<BH_Bullet>();
            if (bullet != null) {
                player.Damage(1);
            }
        }
    }
}

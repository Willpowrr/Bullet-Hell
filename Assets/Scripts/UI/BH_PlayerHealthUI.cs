using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_PlayerHealthUI : MonoBehaviour {

        [SerializeField]
        protected BH_PlayerHealthIcon iconPrefab;
        [SerializeField]
        protected Transform layoutTransform;

        protected List<BH_PlayerHealthIcon> healthIcons = new List<BH_PlayerHealthIcon>();
        
        void Start() {
        }
        
        void Update() {
        }
        
        public void Initialize(int p_health) {

            healthIcons.Clear();
            for (int i = 0; i < p_health; ++i) {
                BH_PlayerHealthIcon iconInstance = Instantiate(iconPrefab, layoutTransform);
                iconInstance.transform.localScale = Vector3.one;
                iconInstance.transform.localPosition = Vector3.zero;
                healthIcons.Add(iconInstance);
            }
        }

        public void UpdateHealth(int p_health) {
            
            for (int i = 0; i < healthIcons.Count; ++i) {
                BH_PlayerHealthIcon icon = healthIcons[i];
                if (p_health > i) {
                    icon.Activate();
                }
                else {
                    icon.Deactivate();
                }
            }
        }
    }
}
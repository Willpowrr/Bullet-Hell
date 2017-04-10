using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_EnemyController : MonoBehaviour {
        
        public BH_GameplayController gameplayController { get; protected set; }

        [SerializeField]
        protected int startSeed = 0;
        
        [SerializeField]
        protected int enemyCacheSize = 10;

        [SerializeField]
        protected Vector3 enemyInactivePosition = new Vector3(0.0f, 100.0f, 0.0f);

        [SerializeField]
        protected List<BH_Enemy> enemyTypes = new List<BH_Enemy>();

        [SerializeField]
        protected float spawnMaxYPosition = 4.0f;
        [SerializeField]
        protected float spawnMinYPosition = -4.0f;
        [SerializeField]
        protected float spawnXPosition = 10.0f;
        [SerializeField]
        protected float enemyMoveSpeed = 7.5f;
        [SerializeField]
        protected AnimationCurve spawnFrequencyCurve;
        [SerializeField]
        protected float spawnFrequencyMax;
        [SerializeField]
        protected float spawnFrequencyScale;

        protected float lastSpawnTime = 0.0f;
        protected Dictionary<string, List<BH_Enemy>> activeEnemies = new Dictionary<string, List<BH_Enemy>>();
        protected List<BH_Enemy> removeList = new List<BH_Enemy>();
        public bool canSpawnEnemies { get; set; }
        public float startTime { get; protected set; }

        #region Unity Functions

        private void Awake() {

            foreach (BH_Enemy enemy in enemyTypes) {
                activeEnemies.Add(enemy.id, new List<BH_Enemy>());
            }
            
            gameplayController = FindObjectOfType<BH_GameplayController>();
        }

        // Use this for initialization
        void Start() {
            Reset();
        }

        // Update is called once per frame
        void Update() {

            if (canSpawnEnemies) {
                float currentTime = Time.fixedTime;


                float spawnFrequency = spawnFrequencyCurve.Evaluate(Mathf.Min((currentTime - startTime) * spawnFrequencyScale, 1.0f));
                if (currentTime - lastSpawnTime > spawnFrequency) {
                    SpawnEnemy();
                    lastSpawnTime = currentTime;
                }
            }

            CheckActiveEnemiesOffscreen();
            CheckActiveEnemiesEnteredScreen();
        }

        #endregion

        protected void CheckActiveEnemiesOffscreen() {

            removeList.Clear();
            foreach (BH_Enemy enemyPrefab in enemyTypes) {
                foreach (BH_Enemy enemyInstance in activeEnemies[enemyPrefab.id]) {
                    if (enemyInstance.enteredScreen && !gameplayController.IsOnScreen(enemyInstance.transform)) {
                        removeList.Add(enemyInstance);
                    }
                }
            }
            ClearRemoveList();
        }

        protected void CheckActiveEnemiesEnteredScreen() {
            foreach (BH_Enemy enemyPrefab in enemyTypes) {
                foreach (BH_Enemy enemyInstance in activeEnemies[enemyPrefab.id]) {
                    if (!enemyInstance.enteredScreen && gameplayController.IsOnScreen(enemyInstance.transform)) {
                        enemyInstance.enteredScreen = true;
                    }
                }
            }
        }

        public void Reset() {
            canSpawnEnemies = false;
            lastSpawnTime = 0.0f;
            ReturnAllEnemies();
            Random.InitState(startSeed);
            removeList.Clear();
        }

        public void InitGameplay() {
            startTime = Time.fixedTime;
        }

        protected void SpawnEnemy() {
            int enemyIndex = Random.Range(0, enemyTypes.Count);
            BH_Enemy enemyPrefab = enemyTypes[enemyIndex];
            BH_Enemy enemyInstance = FastPoolManager.GetPool(enemyPrefab, false).FastInstantiate<BH_Enemy>();
            enemyInstance.prefab = enemyPrefab;
            enemyInstance.enemyController = this;
            float yPosition = Random.Range(spawnMinYPosition, spawnMaxYPosition);
            enemyInstance.transform.position = new Vector3(spawnXPosition, yPosition, 0.0f);
            enemyInstance.Spawn();
            activeEnemies[enemyPrefab.id].Add(enemyInstance);
        }

        protected void ReturnAllEnemies() {
            removeList.Clear();
            foreach (BH_Enemy enemyPrefab in enemyTypes) {
                foreach (BH_Enemy enemy in activeEnemies[enemyPrefab.id]) {
                    removeList.Add(enemy);
                }
            }
            ClearRemoveList();
        }

        protected void ClearRemoveList() {
            
            foreach (BH_Enemy enemy in removeList) {
                ReturnEnemy(enemy);
            }
            removeList.Clear();
        }

        public void ReturnEnemy(BH_Enemy p_enemy) {
            p_enemy.active = false;
            p_enemy.transform.position = enemyInactivePosition;
            p_enemy.rigidBody.velocity = Vector3.zero;
            activeEnemies[p_enemy.id].Remove(p_enemy);
            FastPoolManager.GetPool(p_enemy.prefab, false).FastDestroy(p_enemy.gameObject);
        }
    }
}

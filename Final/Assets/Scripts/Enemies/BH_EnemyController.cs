using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell {
    public class BH_EnemyController : MonoBehaviour {

        [SerializeField]
        protected int startSeed = 0;
        
        [SerializeField]
        protected int enemyCacheSize = 10;

        [SerializeField]
        protected Vector3 enemyInactivePosition = new Vector3(0.0f, 100.0f, 0.0f);

        [SerializeField]
        protected List<BH_Enemy> enemyTypes = new List<BH_Enemy>();

        [SerializeField]
        protected float spawnFrequency = 5.0f;

        [SerializeField]
        protected float spawnMaxYPosition = 4.0f;
        [SerializeField]
        protected float spawnMinYPosition = -4.0f;
        [SerializeField]
        protected float spawnXPosition = 10.0f;

        protected float lastSpawnTime = 0.0f;
        protected Dictionary<string, List<BH_Enemy>> activeEnemies = new Dictionary<string, List<BH_Enemy>>();
        protected Dictionary<string, Queue<BH_Enemy>> enemyCaches = new Dictionary<string, Queue<BH_Enemy>>();

        protected List<BH_Enemy> removeList = new List<BH_Enemy>();

        #region Unity Functions

        private void Awake() {
            CreateEnemyCaches();
            Reset();
        }

        // Use this for initialization
        void Start() {
            lastSpawnTime = 0.0f;
        }

        // Update is called once per frame
        void Update() {

            float currentTime = Time.fixedTime;
            if (currentTime - lastSpawnTime > spawnFrequency) {
                SpawnEnemy();
                lastSpawnTime = currentTime;
            }

            CheckActiveEnemyLifeTimes();
        }

        #endregion

        protected void CheckActiveEnemyLifeTimes() {

            removeList.Clear();
            foreach (BH_Enemy enemyPrefab in enemyTypes) {
                foreach (BH_Enemy enemyInstance in activeEnemies[enemyPrefab.enemyID]) {
                    if (enemyInstance.livedItsLife) {
                        removeList.Add(enemyInstance);
                    }
                }
            }
            ClearRemoveList();
        }

        protected void Reset() {
            lastSpawnTime = 0.0f;
            ReturnAllEnemies();
            Random.InitState(startSeed);
            removeList.Clear();
        }

        protected void SpawnEnemy() {
            int enemyIndex = Random.Range(0, enemyTypes.Count);
            BH_Enemy enemyPrefab = enemyTypes[enemyIndex];
            BH_Enemy enemyInstance = enemyCaches[enemyPrefab.enemyID].Dequeue();
            enemyInstance.Spawn();
            float yPosition = Random.Range(spawnMinYPosition, spawnMaxYPosition);
            enemyInstance.movementController.position = new Vector3(spawnXPosition, yPosition, 0.0f);
            enemyInstance.movementController.velocity = new Vector3(-0.1f, 0.0f, 0.0f);
            activeEnemies[enemyPrefab.enemyID].Add(enemyInstance);
        }

        protected void CreateEnemyCaches() {

            enemyCaches.Clear();
            activeEnemies.Clear();

            foreach (BH_Enemy enemyPrefab in enemyTypes) {
                enemyCaches.Add(enemyPrefab.enemyID, new Queue<BH_Enemy>());
                activeEnemies.Add(enemyPrefab.enemyID, new List<BH_Enemy>());
                
                for (int i = 0; i < enemyCacheSize; ++i) {
                    BH_Enemy enemyInstance = Instantiate(enemyPrefab, transform);
                    enemyInstance.movementController.position = enemyInactivePosition;
                    enemyInstance.movementController.velocity = Vector3.zero;
                    enemyInstance.active = false;
                    enemyCaches[enemyPrefab.enemyID].Enqueue(enemyInstance);
                }
            }
        }

        protected void ReturnAllEnemies() {
            removeList.Clear();
            foreach (BH_Enemy enemyPrefab in enemyTypes) {
                foreach (BH_Enemy enemy in activeEnemies[enemyPrefab.enemyID]) {
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

        protected void ReturnEnemy(BH_Enemy p_enemy) {
            p_enemy.active = false;
            p_enemy.movementController.position = enemyInactivePosition;
            p_enemy.movementController.velocity = Vector3.zero;
            activeEnemies[p_enemy.enemyID].Remove(p_enemy);
            enemyCaches[p_enemy.enemyID].Enqueue(p_enemy);
        }
    }
}

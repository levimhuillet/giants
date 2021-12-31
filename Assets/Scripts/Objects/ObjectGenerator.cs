using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class ObjectGenerator : MonoBehaviour {
        [SerializeField]
        private GameObject m_objectPrefab;
        [SerializeField]
        private Transform m_focalPoint;

        [SerializeField]
        private int m_minSpawnTime, m_maxSpawnTime;
        private float m_spawnTimer;

        public List<GameObject> m_spawnedObjs;

        private void Start() {
            GenerateNewSpawnTime();
        }

        private void Update() {
            if (GameManager.instance.IsPaused) {
                return;
            }

            // decrement timer
            m_spawnTimer -= Time.deltaTime;

            // check if timer is over
            if (m_spawnTimer <= 0) {
                // if so, spawn obj and reset timer
                SpawnNewObj();

                GenerateNewSpawnTime();
            }

            // Remove any and all objects that are past the bounds
            for (int i = 0; i < m_spawnedObjs.Count; ++i) {
                GameObject obj = m_spawnedObjs[i];
                if (obj.transform.position.z >= 0) {
                    Destroy(obj.gameObject);
                    m_spawnedObjs.Remove(obj);
                    --i;
                }
            }
        }

        private void GenerateNewSpawnTime() {
            m_spawnTimer = Random.Range((float)m_minSpawnTime, m_maxSpawnTime);
        }

        private void SpawnNewObj() {
            GameObject newObject = Instantiate(m_objectPrefab, this.transform);
            newObject.GetComponent<WallObject>().SetDir(m_focalPoint);

            m_spawnedObjs.Add(newObject);
        }

        public void ThrowWallObject() {
            int count = m_spawnedObjs.Count;
            if (count > 0) {
                GameObject objToThrow = m_spawnedObjs[count - 1]; // most recent obj
                m_spawnedObjs.Remove(objToThrow);

                Destroy(objToThrow);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class ObjectGenerator : MonoBehaviour {
        [SerializeField]
        private GameObject m_objectPrefab;
        [SerializeField]
        private Transform m_focalPoint;

        public List<GameObject> m_spawnedObjs;

        private void Start() {
            GameObject newObject = Instantiate(m_objectPrefab, this.transform);
            newObject.GetComponent<WallObject>().SetDir(m_focalPoint);

            m_spawnedObjs.Add(newObject);
        }

        private void Update() {
            if (GameManager.instance.IsPaused) {
                return;
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

        public void ThrowWallObject() {
            if (m_spawnedObjs.Count > 0) {
                GameObject objToThrow = m_spawnedObjs[0];
                m_spawnedObjs.Remove(objToThrow);

                Destroy(objToThrow);
            }
        }
    }
}
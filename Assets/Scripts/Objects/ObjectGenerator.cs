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

        public enum Pos {
            left,
            right
        }

        [SerializeField]
        private Pos m_pos;

        public List<WallObject> m_spawnedObjs;

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
                WallObject obj = m_spawnedObjs[i];
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
            WallObject newWallObj = newObject.GetComponent<WallObject>();
            newWallObj.SetDir(m_focalPoint);
            newWallObj.Thrown = false;

            m_spawnedObjs.Add(newWallObj);
        }

        public void ThrowWallObject() {
            int count = m_spawnedObjs.Count;
            if (count > 0) {
                WallObject objToThrow = m_spawnedObjs[count - 1]; // most recent obj

                ConvertPosRelativeToGiant(objToThrow);
                objToThrow.MarkThrown();

                m_spawnedObjs.Remove(objToThrow);

                // Destroy(objToThrow);
            }
        }

        private void ConvertPosRelativeToGiant(WallObject objToThrow) {
            // find how far obj has traveled
            float relProg = GetObjRelativeProgress(objToThrow.gameObject);

            // find the corresponding location along Giant's path
            Vector3 throwLocation = RunManager.instance.CalcThrowLocation(m_pos, relProg);

            // place the obj on the corresponding location
            objToThrow.transform.position = throwLocation;
            objToThrow.SetDir(-RunManager.instance.GetGiantDir(m_pos));

            // scale obj accordingly

        }

        private float GetObjRelativeProgress(GameObject objToThrow) {
            Vector3 totalVector = this.transform.position - m_focalPoint.position;
            float totalDist = totalVector.magnitude;

            Vector3 objVector = objToThrow.transform.position - m_focalPoint.position;
            float objDist = objVector.magnitude;

            return objDist / totalDist;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class ObjectGenerator : MonoBehaviour {
        [SerializeField]
        private GameObject m_objectPrefab;
        [SerializeField]
        private Transform m_focalPoint;

        private void Start() {
            GameObject newObject = Instantiate(m_objectPrefab, this.transform);
            newObject.GetComponent<WallObject>().SetDir(m_focalPoint);
        }
    }
}
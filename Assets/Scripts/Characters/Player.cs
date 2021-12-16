using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class Player : MonoBehaviour {
        public static Player instance;

        [SerializeField]
        private int m_catchDistance;

        private void OnEnable() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }
            m_catchDistance *= -1; // camera is oriented backwards
        }

        public int GetCatchDistance() {
            return m_catchDistance;
        }
    }
}

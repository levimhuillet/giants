using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class GameManager : MonoBehaviour {
        public static GameManager instance;

        #region Unity Callbacks

        private void OnEnable() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }
        }

        #endregion
    }
}
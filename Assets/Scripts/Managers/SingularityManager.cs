using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Giants {
    public class SingularityManager : MonoBehaviour {
        private void Start() {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

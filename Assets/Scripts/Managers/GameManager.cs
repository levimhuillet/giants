using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class GameManager : MonoBehaviour {
        public static GameManager instance;

        private bool m_isPaused;

        public bool IsPaused { get { return m_isPaused; } }

        #region Unity Callbacks

        private void OnEnable() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }

            m_isPaused = false;

            EventManager.OnPause.AddListener(HandleOnPause);
            EventManager.OnResume.AddListener(HandleOnResume);
            EventManager.OnGameOver.AddListener(HandleOnGameOver);
            EventManager.OnRestart.AddListener(HandleOnRestart);
            EventManager.OnReturnMain.AddListener(HandleOnReturnMain);
        }

        #endregion

        private void BeginPause() {
            m_isPaused = true;
        }

        private void EndPause() {
            m_isPaused = false;
        }

        private void HandleOnPause() {
            BeginPause();
        }

        private void HandleOnGameOver() {
            BeginPause();
        }

        private void HandleOnResume() {
            EndPause();
        }

        private void HandleOnRestart() {
            EndPause();
        }

        private void HandleOnReturnMain() {
            EndPause();
        }
    }
}
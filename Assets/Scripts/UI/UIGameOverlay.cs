using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Giants {
    public class UIGameOverlay : MonoBehaviour {
        [SerializeField]
        private Button m_pauseButton;

        [SerializeField]
        private GameObject m_pauseMenu;

        [SerializeField]
        private GameObject m_gameOverMenu;

        private void OnEnable() {
            m_pauseButton.onClick.AddListener(HandleOnPause);
            EventManager.OnGameOver.AddListener(HandleOnGameOver);
        }

        private void OnDisable() {
            m_pauseButton.onClick.RemoveListener(HandleOnPause);
            EventManager.OnGameOver.RemoveListener(HandleOnGameOver);
        }

        private void HandleOnPause() {
            m_pauseMenu.SetActive(true);

            EventManager.OnPause.Invoke();
        }

        private void HandleOnGameOver() {
            m_gameOverMenu.SetActive(true);
        }
    }
}


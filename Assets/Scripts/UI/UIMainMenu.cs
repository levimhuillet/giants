using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Giants {
    public class UIMainMenu : MenuBase {
        #region Editor

        [SerializeField]
        private Button m_newGameButton;
        [SerializeField]
        private Button m_quitButton;

        #endregion

        #region Unity Callbacks

        private void Awake() {
            m_newGameButton.onClick.AddListener(HandleNewGame);
            m_quitButton.onClick.AddListener(HandleQuit);
        }

        #endregion

        #region ButtonHandlers

        private void HandleNewGame() {
            AudioManager.instance.PlayAudio("giants", true);

            SceneManager.LoadScene("Run");
        }

        private void HandleQuit() {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        #endregion  
    }
}

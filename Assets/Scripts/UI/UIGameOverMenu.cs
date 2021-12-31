using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Giants {
    public class UIGameOverMenu : MenuBase {
        [SerializeField]
        private Button m_returnMainButton;
        [SerializeField]
        private Button m_runAgainButton;

        private void OnEnable() {
            base.EnableMenu();

            // set button handlers
            m_runAgainButton.onClick.AddListener(HandleRunAgain);
            m_returnMainButton.onClick.AddListener(HandleReturnMain);
        }

        private void OnDisable() {
            base.DisableMenu();
        }

        private void HandleRunAgain() {

        }

        private void HandleReturnMain() {

        }
    }
}

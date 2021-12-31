using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Giants {
    public class UIPauseMenu : MenuBase {
        [SerializeField]
        private Button m_closeButton;

        private void OnEnable() {
            base.EnableMenu();

            // set button handlers
            m_closeButton.onClick.AddListener(HandleOnClose);
        }

        private void OnDisable() {
            base.DisableMenu();
        }

        private void HandleOnClose() {
            this.gameObject.SetActive(false);

            EventManager.OnResume.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Giants {
    public class MenuBase : MonoBehaviour {
        [SerializeField]
        private Button[] m_buttons;

        public Button[] MenuButtons {
            get { return m_buttons; }
        }
    }
}

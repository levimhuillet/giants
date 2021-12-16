﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    [RequireComponent(typeof(Animator))]
    public class Giant : MonoBehaviour {
        private Animator m_animator;

        [SerializeField]
        private bool m_isStaggered;
        [SerializeField]
        private float m_speed;
        [SerializeField]
        private float m_magRate;
        [SerializeField]
        private float m_gapBetweenGiants;

        private float m_startScale;
        private int m_playerCatchDistance;
        private float m_moveInwardRatio;

        private void OnEnable() {
            m_animator = this.GetComponent<Animator>();

            m_animator.SetBool("isStaggered", m_isStaggered);

            m_startScale = this.transform.localScale.x;

            m_playerCatchDistance = Player.instance.GetCatchDistance();

            if (this.transform.position.x < 0) {
                m_gapBetweenGiants *= -1;
            }
            m_moveInwardRatio = (this.transform.position.x - m_gapBetweenGiants) / m_playerCatchDistance;
        }

        private void Update() {
            // move closer to camera
            Vector3 currPos = this.transform.position;
            Vector3 newPos = currPos + new Vector3(m_moveInwardRatio, 0, -m_speed) * Time.deltaTime;

            this.transform.position = newPos;

            // increase magnification
            //float currMag = m_startScale + Mathf.Pow(this.transform.position.z * m_magRate, 2);
            //this.transform.localScale = new Vector3(currMag, currMag, 1);

            if (this.transform.position.z <= m_playerCatchDistance) {
                Debug.Log("Caught!");
            }
        }
    }
}

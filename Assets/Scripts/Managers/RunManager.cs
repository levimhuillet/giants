using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class RunManager : MonoBehaviour {
        public static RunManager instance;

        [SerializeField]
        private int m_playerCatchDistance = 19;
        [SerializeField]
        private float m_gapBetweenGiants = 0.5f;
        [SerializeField]
        private float m_maxProgressToThrow = 0.9f;
        [SerializeField]
        private Giant m_leftGiant;
        [SerializeField]
        private Giant m_rightGiant;

        private void OnEnable() {
            if (instance == null) {
                instance = this;
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }

            m_playerCatchDistance *= -1; // camera is oriented backwards
        }

        public int GetPlayerCatchDistance() {
            return m_playerCatchDistance;
        }

        public float GetGapBetweenGiants() {
            return m_gapBetweenGiants;
        }

        public float GetMaxProgToThrow() {
            return m_maxProgressToThrow;
        }

        public Vector3 CalcThrowLocation(ObjectGenerator.Pos pos, float relativeProgress) {
            Vector3 throwLocation = Vector3.zero;
            Giant.Track track;

            switch (pos) {
                case ObjectGenerator.Pos.left:
                    track = m_leftGiant.GetTrack();
                    break;
                case ObjectGenerator.Pos.right:
                    track = m_rightGiant.GetTrack();
                    break;
                default:
                    Debug.Log("unknown generator when throwing - should not occur");
                    return throwLocation;
            }

            throwLocation = track.StartPos + new Vector3(
                track.MoveVector.x,
                track.MoveVector.y,
                track.MoveVector.z * relativeProgress
                );

            return throwLocation;
        }

        public Vector3 GetGiantDir(ObjectGenerator.Pos pos) {
            switch (pos) {
                case ObjectGenerator.Pos.left:
                    return m_leftGiant.GetTrack().MoveVector.normalized;
                case ObjectGenerator.Pos.right:
                    return m_rightGiant.GetTrack().MoveVector.normalized;
                default:
                    Debug.Log("unknown generator when getting Giant movement dir - should not occur");
                    return Vector3.zero;
            }
        }
    }
}

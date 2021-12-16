using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    /// <summary>
    /// Interface for classes with complex audio functionality
    /// </summary>
    public interface IAudioPlayer {
        /// <summary>
        /// For longer sounds
        /// </summary>
        /// <param name="clipID"></param>
        void PlayAudio(string clipID, bool loop = false);

        /// <summary>
        /// Returns whether audio is being played
        /// </summary>
        /// <returns></returns>
        bool IsPlayingAudio();

        /// <summary>
        /// Stops the current audio
        /// </summary>
        void StopAudio();

        /// <summary>
        /// Pauses the current audio
        /// </summary>
        void PauseAudio();

        /// <summary>
        /// Unpauses the current audio
        /// </summary>
        void UnPauseAudio();

        /// <summary>
        /// Resumes previously stopped audio
        /// </summary>
        void ResumeAudio();

        /// <summary>
        /// Saves the current audio for later
        /// </summary>
        void StashAudio();

        /// <summary>
        /// Resumes previously stashed audio
        /// </summary>
        void ResumeStashedAudio();
    }
}
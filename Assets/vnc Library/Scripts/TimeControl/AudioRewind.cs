using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using vnc.TimeSystem;

namespace vnc.TimeSystem
{
    [System.Serializable]
    public class AudioRewind
    {
        [SerializeField] private AudioInfo[] audios;

        public void Record()
        {
            foreach (var audioInfo in audios)
            {
                if (audioInfo.stateList == null)
                    audioInfo.stateList = new List<AudioState>();

                if (audioInfo.stateList.Count > 0)
                {
                    SetPlaystate(audioInfo);
                    //var lastState = audioInfo.stateList.Last();
                    //if (lastState.isPlaying 
                    //    && !audioInfo.source.isPlaying
                    //    && audioInfo.source.time < audioInfo.source.clip.length)
                    //{
                    //    audioInfo.source.Play();
                    //    audioInfo.source.time = lastState.timePosition;
                    //}
                }

                audioInfo.source.pitch = 1;
                audioInfo.stateList.Add(new AudioState
                {
                    isPlaying = audioInfo.source.isPlaying,
                    timePosition = audioInfo.source.time
                });

                if (audioInfo.stateList.Count > TimeControl.TimeSnapshots)
                    audioInfo.stateList.RemoveAt(0);
            }
        }

        public void Rewind()
        {
            foreach (var audioInfo in audios)
            {
                audioInfo.source.pitch = -1;
                if (audioInfo.stateList.Count > 1)
                {
                    audioInfo.stateList.RemoveAt(audioInfo.stateList.Count - 1);
                    SetPlaystate(audioInfo);
                }
                else
                {
                    audioInfo.source.Pause();
                }
            }
        }

        public void SetPlaystate(AudioInfo audioInfo)
        {
            var lastState = audioInfo.stateList.Last();
            if (lastState.isPlaying
                && !audioInfo.source.isPlaying
                && audioInfo.source.time < audioInfo.source.clip.length)
            {
                audioInfo.source.Play();
                audioInfo.source.time = lastState.timePosition;
            }
        }
    }

    [System.Serializable]
    public class AudioInfo
    {
        public AudioSource source;
        public List<AudioState> stateList;
    }

    [System.Serializable]
    public struct AudioState
    {
        public bool isPlaying;
        public float timePosition;
    }
}

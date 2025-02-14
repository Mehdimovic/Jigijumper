﻿using System.Collections;
using UnityEngine;
using JigiJumper.Managers;


namespace JigiJumper.Spawner {
    public class SoundPlayerPool : PersistentBehavior<SoundPlayerPool> {
        [SerializeField] AudioSource _audioPrefab = null;
        PoolSystem<AudioSource> _pool;
        Transform _transform;

        protected override void OnAwake() {
            _transform = transform;

            var preAudioSources = GetComponentsInChildren<AudioSource>();
            if (preAudioSources != null && preAudioSources.Length > 0) {
                _pool = new PoolSystem<AudioSource>(_audioPrefab, preAudioSources);
            } else {
                _pool = new PoolSystem<AudioSource>(_audioPrefab);
            }
        }

        public void PlayMusic(AudioClip clip) {
            AudioSource audio = _pool.Spawn(Vector3.zero, Quaternion.identity, _transform);
            audio.clip = clip;
            audio.Play();
            StartCoroutine(DespawnAudioSource(audio, clip.length));
        }

        public AudioSource ManualPlayMusic(AudioClip clip) {
            AudioSource audio = _pool.Spawn(Vector3.zero, Quaternion.identity, _transform);
            audio.clip = clip;
            audio.Play();
            return audio;
        }

        public void ManualDespawn(AudioSource audio) {
            audio.Stop();
            _pool.Despawn(audio);
        }

        public void StopAllAudios(AudioSource[] audios) {
            if (audios == null || audios.Length < 1) { return; }

            foreach (var audio in audios) {
                if (audio == null) continue;
                audio.Stop();
                ManualDespawn(audio);
            }
        }

        IEnumerator DespawnAudioSource(AudioSource audio, float time) {
            yield return new WaitForSeconds(time);
            audio.Stop();
            _pool.Despawn(audio);
        }
    }
}
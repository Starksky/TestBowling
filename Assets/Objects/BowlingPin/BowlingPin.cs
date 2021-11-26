using System;
using UnityEngine;
using Varwin;
using Varwin.Public;

namespace Varwin.Types.BowlingPin_c70f9816f3bf428093b9a1328f515f0f
{
    [VarwinComponent(English: "BowlingPin", Russian: "Кегля для боулинга")]
    public class BowlingPin : VarwinObject
    {
        [Checker(English: "Is Costs", Russian: "Стоит")]
        public bool IsCosts()
        {
            return !_isFall;
        }

        [Event(English: "OnFall", Russian: "Упало")]
        public event Action OnFall;

        [SerializeField] private AudioClip soundHit;
        [SerializeField] private ParticleSystem psHit;
        
        private AudioSource _audioSource;
        private Rigidbody _rigidbody;
        private bool _isFall;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = soundHit;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(!_audioSource.isPlaying)
                _audioSource.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isFall) return;
            OnFall?.Invoke();
            psHit.Play();
            _isFall = true;
        }
    }
}

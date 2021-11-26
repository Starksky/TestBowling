using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Varwin;
using Varwin.Public;
using Random = System.Random;

namespace Varwin.Types.BowlingBall_b36a3390944b4875b9a05613018ab49c
{
    [RequireComponent(typeof(Rigidbody))]
    [VarwinComponent(English: "BowlingBall", Russian: "Шар для боулинга")]
    public class BowlingBall : VarwinObject, IGrabStartAware, IGrabEndAware
    {
        private float _force = 1f;
        private bool _isEndGrub;
        
        [Variable(English: "Drag", Russian: "Сопротивление")]
        public float Drag
        {
            get => _rigidbody.drag;
            internal set => _rigidbody.drag = value;
        }
        
        [Variable(English: "Drag Angular", Russian: "Сопротивление угловое")]
        public float AngularDrag
        {
            get => _rigidbody.angularDrag;
            internal set => _rigidbody.angularDrag = value;
        }
        
        [Variable(English: "Dynamic Friction", Russian: "Динамическое трение")]
        public float DynamicFriction
        {
            get => _sphereCollier.material.dynamicFriction;
            internal set {
                _sphereCollier.material.dynamicFriction = value;
                //UpdateMaterial();
            }
        }
        
        [Variable(English: "Static Friction", Russian: "Статическое трение")]
        public float StaticFriction
        {
            get => _sphereCollier.material.staticFriction ;
            internal set {
                _sphereCollier.material.staticFriction = value;
                //UpdateMaterial();
            }
        }
        
        [Variable(English: "Bounciness", Russian: "Прыгучесть")]
        public float Bounciness
        {
            get => _sphereCollier.material.bounciness;
            internal set {
                _sphereCollier.material.bounciness = value;
                //UpdateMaterial();
            }
        }
        
        [Variable(English: "Mass", Russian: "Масса шара")]
        public float Mass
        {
            get => _rigidbody.mass;
            internal set => _rigidbody.mass = value;
        }
        
        [Variable(English: "Force", Russian: "Сила броска")]
        public float Force
        {
            get => _force;
            internal set => _force = value;
        }
        
        [SerializeField] private AudioClip soundRoll;
        
        private Rigidbody _rigidbody;
        private SphereCollider _sphereCollier;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _sphereCollier = GetComponent<SphereCollider>();
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();

            _audioSource.clip = soundRoll;
        }

        private void UpdateMaterial()
        {
            _sphereCollier.enabled = false;
            _sphereCollier.enabled = true;
        }
        
        public void OnGrabStart(GrabingContext context)
        {
            _isEndGrub = false;
            _audioSource.Stop();
        }

        public void OnGrabEnd()
        {
            _rigidbody.AddTorque(new Vector3(UnityEngine.Random.Range(-10, 10) / 10f, 1f, 1f) * _force / 10f, ForceMode.Impulse);
            _rigidbody.AddForce(Camera.current.transform.forward * _force, ForceMode.Impulse);
            
            _isEndGrub = true;
        }

        public void OnCollisionEnter(Collision other)
        {
            if(_isEndGrub)
            {
                _audioSource.Play();
                _isEndGrub = false;
            }
        }
    }
}

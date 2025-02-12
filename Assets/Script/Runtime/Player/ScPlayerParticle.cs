using System;
using Dindio.Runtime.Interactable.Inventory;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dindio.Runtime.Player {
    public class ScPlayerParticle : NetworkBehaviour {
        
        [Header("Particle Systems")]
        [SerializeField] ScPlayerParticleRenderer _boostParticle;
        [SerializeField] ScPlayerParticleRenderer _consumableParticle;

        public void StartParticle(Color particleColor, float particleDuration, bool isBoost = false) {
            ScPlayerParticleRenderer currentParticle = isBoost? _boostParticle : _consumableParticle;
            currentParticle.StartAnim(particleColor, particleDuration);

        }


    }
}
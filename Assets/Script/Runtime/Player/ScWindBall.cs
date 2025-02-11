using System;
using System.Collections;
using System.Collections.Generic;
using Dindio.Runtime.Interfaces;
using Dindio.Runtime.Player.Anim;
using Unity.Netcode;
using UnityEngine;
using static Dindio.Runtime.Others.ScUtils;

namespace Dindio.Runtime.Player {
    public class ScWindBall : NetworkBehaviour {
        private NetworkVariable<int> _damage = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        private NetworkVariable<ulong> _ownerClientId = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        private Coroutine _coDamaging;
        
        public void Initialize(ulong ownerClientId, int damage) {
            _ownerClientId.Value = ownerClientId;
            _damage.Value = damage;
            _colliders.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.transform.parent || !other.GetComponentInParent<NetworkObject>()) {
                return;
            }

            NetworkObject hitNetworkObject = other.GetComponentInParent<NetworkObject>();
            Debug.Log($"[Collision] Owner du projectile: {_ownerClientId.Value}, Owner touché: {hitNetworkObject.OwnerClientId}");

            if (hitNetworkObject.OwnerClientId == _ownerClientId.Value) {
                return;
            }

            if (!other.transform.parent.TryGetComponent(out IHealth health)) {
                return;
            }
            
            Debug.Log("GivingDamage to : " + other.transform.parent.name + "amount : " + _damage.Value);
            _coDamaging = StartCoroutine(Damaging(other, health));
            
            
            
        }

        IEnumerator Damaging(Collider2D other, IHealth health) {
          yield return new WaitForSeconds(0.5f);
          health.TakeDamage(_damage.Value);
          other.GetComponentInParent<ScPlayerAnim>().PlayHit();
        }

        private void OnTriggerExit2D(Collider2D other) {
            StopCoroutine(_coDamaging);
        }
    }
}

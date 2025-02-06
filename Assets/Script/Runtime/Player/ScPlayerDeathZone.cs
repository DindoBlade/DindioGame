using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Player {



public class ScPlayerDeathZone : NetworkBehaviour {
    private bool _inDeathZone = false;

    [SerializeField] private float _deathTimerTime  = 10f;
    [SerializeField] private float _emptyTimerAfter = 10f;

    [SerializeField] private float _inDeathZoneTime    = 0f;
    [SerializeField] private float _outOfDeathZoneTime = 0f;

    public override void OnNetworkSpawn() {
        if (!IsOwner) {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_inDeathZone)
        {
            if ((_inDeathZoneTime += Time.deltaTime) >= _deathTimerTime)
            {
                ScPlayerHealth health = gameObject.GetComponent<ScPlayerHealth>();
                health.TakeDamage(health.MaxHp);
                OutOfDeathZone();
            }
        }
        else
        {
            if ((_outOfDeathZoneTime += Time.deltaTime) >= _emptyTimerAfter)
            {
                _inDeathZoneTime -= Time.deltaTime;
            }
        }
    }

    public void InDeathZone()
    {
        _inDeathZone = true;
        _outOfDeathZoneTime = 0f;

        if (_inDeathZoneTime < 0f)
            _inDeathZoneTime = 0f;
    }

    public void OutOfDeathZone()
    {
        _inDeathZone = false;
    }

}



}

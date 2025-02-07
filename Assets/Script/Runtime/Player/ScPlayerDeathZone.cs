using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Player {



public class ScPlayerDeathZone : NetworkBehaviour {
    [SerializeField] private float _deathTimerTime  = 10f;
    [SerializeField] private float _emptyTimerAfter = 10f;

    [SerializeField] private float _inDeathZoneTime    = 0f;
    [SerializeField] private float _outOfDeathZoneTime = 0f;

    private Coroutine _inDeathZoneCoroutine = null;
    private Coroutine _outOfDeathZoneCoroutine = null;

    public override void OnNetworkSpawn() {
        if (!IsOwner) {
            gameObject.SetActive(false);
        }
    }

    public void InDeathZone()
    {
        _outOfDeathZoneTime = 0f;

        if (_inDeathZoneTime < 0f)
            _inDeathZoneTime = 0f;
        
        if (_outOfDeathZoneCoroutine != null)
            StopCoroutine(_outOfDeathZoneCoroutine);
        _outOfDeathZoneCoroutine = null;
        _inDeathZoneCoroutine = StartCoroutine(InDeathZoneCoroutine());
    }

    public void OutOfDeathZone()
    {

        if (_inDeathZoneCoroutine != null)
            StopCoroutine(_inDeathZoneCoroutine);
        _inDeathZoneCoroutine = null;
        _outOfDeathZoneCoroutine = StartCoroutine(OutOfDeathZoneCoroutine());
    }

    private IEnumerator InDeathZoneCoroutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();


            _inDeathZoneTime += Time.deltaTime;
            ScCallbacks.OnUpdateDeathZone.Invoke(_inDeathZoneTime, _deathTimerTime);

            if (_inDeathZoneTime >= _deathTimerTime)
            {
                ScPlayerHealth health = gameObject.GetComponent<ScPlayerHealth>();
                health.TakeDamage(health.MaxHp);
                // Debug.Log("dead");
                OutOfDeathZone();
            }
        }
    }

    private IEnumerator OutOfDeathZoneCoroutine()
    {
        float dt;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            dt = Time.deltaTime;

            if ((_outOfDeathZoneTime += dt) >= _emptyTimerAfter)
            {              
                if ((_inDeathZoneTime -= dt) <= 0f)
                {
                    _inDeathZoneTime = 0f;
                    StopCoroutine(_outOfDeathZoneCoroutine);
                }
            }
            ScCallbacks.OnUpdateDeathZone.Invoke(_inDeathZoneTime, _deathTimerTime);
        }
    }

}


}


using System.Collections;
using TMPro;
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

    public void InDeathZone(TMP_Text timer_text)
    {
        _outOfDeathZoneTime = 0f;

        if (_inDeathZoneTime < 0f)
            _inDeathZoneTime = 0f;
        
        if (_outOfDeathZoneCoroutine != null)
            StopCoroutine(_outOfDeathZoneCoroutine);
        _outOfDeathZoneCoroutine = null;
        _inDeathZoneCoroutine = StartCoroutine(InDeathZoneCoroutine(timer_text));
    }

    public void OutOfDeathZone(TMP_Text timer_text)
    {
        if (_inDeathZoneCoroutine != null)
            StopCoroutine(_inDeathZoneCoroutine);
        _inDeathZoneCoroutine = null;
        _outOfDeathZoneCoroutine = StartCoroutine(OutOfDeathZoneCoroutine(timer_text));
    }

    private IEnumerator InDeathZoneCoroutine(TMP_Text timer_text)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if ((_inDeathZoneTime += Time.deltaTime) >= _deathTimerTime)
            {
                Debug.Log("dead");
                ScPlayerHealth health = gameObject.GetComponent<ScPlayerHealth>();
                health.TakeDamage(health.MaxHp);
                OutOfDeathZone(timer_text);
                timer_text.gameObject.SetActive(false);
                yield break;
            }

            UpdateText(timer_text);
        }
    }

    private IEnumerator OutOfDeathZoneCoroutine(TMP_Text timer_text)
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
                    timer_text.gameObject.SetActive(false);
                    yield break;
                }

                UpdateText(timer_text);
            }
        }
    }

    private void UpdateText(TMP_Text text)
    {
        if (text == null) return;

        if (_inDeathZoneTime <= 0f)
        {
            text.gameObject.SetActive(false);
            return;
        }

        text.gameObject.SetActive(true);
        float timeLeft = _deathTimerTime - _inDeathZoneTime;
        text.text = timeLeft.ToString("F3");
    }

}


}
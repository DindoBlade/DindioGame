using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScDeathZoneTimer : NetworkBehaviour
{
    [SerializeField] private ScDeathZone _deathZone;

    [SerializeField] private List<float> _deathZoneSizes = new List<float>();
    [SerializeField] private float _updateEvery = 180f;
    private Coroutine _timerCoroutine = null;
    private int _index = -1;


    public void ResetZone()
    {
        if (!IsServer) return;
        _index = -1;
        StartTimer();
    }

    private void StartTimer()
    {
        if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
        _timerCoroutine = StartCoroutine(DeathZoneTimer());
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        StartTimer();
    }

    private IEnumerator<WaitForSeconds> DeathZoneTimer()
    {
        while (_index < _deathZoneSizes.Count - 1)
        {
            _deathZone.SetSizeServerRpc(_deathZoneSizes[++_index]);
            yield return new WaitForSeconds(_updateEvery);
        }
    }
}

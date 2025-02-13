using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScDeathZoneTimer : NetworkBehaviour
{
    [SerializeField] private ScDeathZone _deathZone;

    [SerializeField] private List<float> _deathZoneSizes = new List<float>();
    [SerializeField] private float _updateEvery = 180f;
    private int _index = -1;

    private void Awake()
    {
        if (!IsServer)
        {
            Destroy(gameObject);
            return;
        }
    }

    public override void OnNetworkSpawn()
    {
        StartCoroutine(DeathZoneTimer());
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

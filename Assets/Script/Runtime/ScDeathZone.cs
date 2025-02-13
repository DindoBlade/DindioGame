using Dindio.Runtime.Player;
using TMPro;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof (BoxCollider2D))]
public class ScDeathZone: NetworkBehaviour
{
    [SerializeField] private TMP_Text deathZoneText;

    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // if (!IsOwner) return;

        if (!collider.gameObject.transform.root.gameObject.TryGetComponent(out ScPlayerDeathZone playerDeathZone))
            return;

        playerDeathZone.InDeathZone(deathZoneText);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if (!IsOwner) return;

        if (!collider.gameObject.transform.root.gameObject.TryGetComponent(out ScPlayerDeathZone playerDeathZone))
            return;

        playerDeathZone.OutOfDeathZone(deathZoneText);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetSizeServerRpc(float size)
    {
        _collider.size = new Vector2(size, size);
    }
}
using Dindio.Runtime.Player;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ScDeathZone: NetworkBehaviour
{
    [SerializeField] private TMP_Text deathZoneText;

    private void Awake()
    {
        // if (!IsOwner)
        // {
        //     gameObject.SetActive(false);
        //     return;
        // }

        ScCallbacks.OnUpdateDeathZone.AddListener(UpdateText);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // if (!IsOwner) return;

        if (!collider.gameObject.transform.root.gameObject.TryGetComponent(out ScPlayerDeathZone playerDeathZone))
            return;
        
        playerDeathZone.InDeathZone();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if (!IsOwner) return;

        if (!collider.gameObject.transform.root.gameObject.TryGetComponent(out ScPlayerDeathZone playerDeathZone))
            return;
        
        playerDeathZone.OutOfDeathZone();
    }

    private void UpdateText(float timer, float totalTime)
    {
        if (timer <= 0f)
        {
            deathZoneText.gameObject.SetActive(false);
            return;
        }

        deathZoneText.gameObject.SetActive(true);
        float timeLeft = totalTime - timer;
        deathZoneText.text = timeLeft.ToString("F2");
    }
}
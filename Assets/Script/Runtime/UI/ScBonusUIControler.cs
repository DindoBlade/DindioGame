using UnityEngine;
using UnityEngine.UI;

public class ScBonusUIController : MonoBehaviour
{ 
    [SerializeField] private Image radialIndicatorUI;
    
    private float maxIndicatorTimer;
    private float indicatorTimer;
    private bool shouldUpdate = false;

    public void BeginTimerUpdate(float maxBonusTimer)
    {
        maxIndicatorTimer = maxBonusTimer;
        indicatorTimer = maxBonusTimer;
        shouldUpdate = true;
    }

    private void Update()
    {
        if (shouldUpdate)
        {
            indicatorTimer -= Time.deltaTime;
            radialIndicatorUI.fillAmount = indicatorTimer / maxIndicatorTimer;
        }
        else if (shouldUpdate && indicatorTimer <= 0f)
        {
            shouldUpdate = false;
        }
    }
}

using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField] LightController lightController;

    [Header("バッテリー容量表示テキスト")]
    [SerializeField] TextMeshProUGUI powerText;

    [Header("バッテリーの最大容量")]
    [SerializeField] float maxPower;

    float powerUsage = 5.0f;   // バッテリー消費量
    float powerUsageSpd = 10.0f; // バッテリー消費速度

    float power;
    public float Power => power;
    public float PowerRate => power / maxPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        power = maxPower;
        PowerToString();
        StartCoroutine(PowerUse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PowerUse()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUsageSpd);

            DecreasePower(powerUsage * (lightController.LightRangeRatio));
            PowerToString();
        }
    }

    public void IncreasePower(float value)
    {
        power += value;
        power = SavePower();
    }

    public void DecreasePower(float value)
    {
        power -= value;
        power = SavePower();
    }

    float SavePower()
    {
        return Mathf.Clamp(power, 0, maxPower);
    }

    void PowerToString()
    {
        powerText.text = "Power: " + (PowerRate * 100f).ToString("F0") + " %";
    }
}

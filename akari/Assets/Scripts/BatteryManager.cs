using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    [SerializeField] LightController lightController;

    [Header("バッテリーアイコン")]
    [SerializeField] Image powerIcon;

    [Header("バッテリー容量表示テキスト")]
    [SerializeField] TextMeshProUGUI powerText;

    [Header("バッテリーの最大容量")]
    [SerializeField] float maxPower;

    [Header("バッテリー消費量")]
    [SerializeField] float powerUsage;

    [Header("バッテリー消費速度")]
    [SerializeField] float powerUsageSpd;

    float power;
    public float Power => power;
    public float PowerRate => power / maxPower;
    public bool HasPower => power > 0;

    [Header("アイコンフェードアウトの有効距離")]
    [SerializeField] float fadeDistance;

    [Header("フェード速度")]
    [SerializeField] float fadeSpeed;

    [Header("フェードアウト時の透明度")]
    [SerializeField] float alpha;

    // Update is called once per frame
    void Update()
    {
        PowerUIFade();
    }

    public IEnumerator PowerUse()
    {
        IncreasePower(maxPower);
        lightController.gameObject.SetActive(true);

        while (true)
        {
            yield return new WaitForSeconds(powerUsageSpd);

            DecreasePower(powerUsage * (lightController.LightRangeRatio));
        }
    }

    public void IncreasePower(float value)
    {
        power += value;
        power = SavePower();
        PowerToText();
    }

    public void DecreasePower(float value)
    {
        power -= value;
        power = SavePower();
        PowerToText();
    }

    float SavePower()
    {
        return Mathf.Clamp(power, 0, maxPower);
    }

    void PowerToText()
    {
        powerText.text = (PowerRate * 100f).ToString("F0") + " %";
    }

    void PowerUIFade()
    {
        var w_iconPos = Camera.main.ScreenToWorldPoint(powerIcon.rectTransform.position);
        if (Vector2.Distance(lightController.LightPos, w_iconPos) <= fadeDistance)
        {
            powerIcon.color = AlphaSetter.FadeOut(powerIcon.color, fadeSpeed, alpha);
            powerText.color = AlphaSetter.FadeOut(powerText.color, fadeSpeed, alpha);
        }
        else if (powerIcon.color.a < 1)
        {
            powerIcon.color = AlphaSetter.FadeIn(powerIcon.color, fadeSpeed);
            powerText.color = AlphaSetter.FadeIn(powerText.color, fadeSpeed);
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("現在スコアテキスト")]
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] LightController lightController;

    [Header("テキストフェードアウトの有効距離")]
    [SerializeField] float fadeDistance;

    [Header("フェード速度")]
    [SerializeField] float fadeSpeed;

    [Header("フェードアウト時の透明度")]
    [SerializeField] float alpha;

    int score = 0;
    public int Score => score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreToText();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTextFade();
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        ScoreToText();
    }

    public void DecreaseScore(int score)
    {
        this.score -= score;
        ScoreToText();
    }

    void ScoreToText()
    {
        scoreText.text = $"{score} Pts";
    }

    void ScoreTextFade()
    {
        var w_iconPos = Camera.main.ScreenToWorldPoint(scoreText.rectTransform.position);
        if (Vector2.Distance(lightController.LightPos, w_iconPos) <= fadeDistance)
        {
            scoreText.color = AlphaSetter.FadeOut(scoreText.color, fadeSpeed, alpha);
        }
        else if (scoreText.color.a < 1)
        {
            scoreText.color = AlphaSetter.FadeIn(scoreText.color, fadeSpeed);
        }
    }
}

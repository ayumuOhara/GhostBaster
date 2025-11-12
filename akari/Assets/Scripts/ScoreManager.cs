using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;
    public int Score => score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
    }

    public void DecreaseScore(int score)
    {
        this.score -= score;
    }
}

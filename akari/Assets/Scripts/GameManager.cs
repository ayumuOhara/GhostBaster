using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BatteryManager batteryManager;
    [SerializeField] Animator startWindowAnimator;

    bool isStart = false;
    bool isEnd = false;
    public bool IsStart => isStart;
    public bool IsEnd => isEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitGameStart());
    }

    IEnumerator WaitGameStart()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        StartCoroutine(batteryManager.PowerUse());
        startWindowAnimator.SetTrigger("Start");
        isStart = true;
    }
}

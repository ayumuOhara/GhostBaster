using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GhostGenerator : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] float generateInterbal;

    List<GhostController> ghostList = new List<GhostController>();

    Vector2 BottomLeft;
    Vector2 TopRight;

    bool isGenerate => ghostList.Count < 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnGhosts()
    {
        yield return new WaitUntil(() => gameManager.IsStart);

        while (true)
        {
            GhostController ghost = Instantiate(ghostPrefab, RandomPos(), Quaternion.identity).GetComponent<GhostController>();
            ghostList.Add(ghost);

            yield return new WaitUntil(() => isGenerate);       // 3ëÃÇ‹Ç≈ê∂ê¨
            yield return new WaitForSeconds(generateInterbal);
        }
    }

    public void RemoveGhost(GhostController ghost)
    {
        ghostList.Remove(ghost);
    }

    Vector3 RandomPos()
    {
        var posX = Random.Range(BottomLeft.x, TopRight.x);
        var posY = Random.Range(BottomLeft.y, TopRight.y);

        return new Vector2(posX, posY);
    }
}

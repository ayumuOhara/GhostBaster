using System.Collections;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    Vector2 BottomLeft;
    Vector2 TopRight;

    float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        Vector3 targetPos = Vector2.zero;
        Vector3 moveDir = Vector2.zero;

        while (true)
        {
            targetPos = NextMovePoint();
            moveDir = (targetPos - transform.position).normalized;

            while (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;

                spriteRenderer.color += new Color(0, 0, 0, -10 * Time.deltaTime);

                yield return null;
            }

            yield return new WaitForSeconds(1.0f);

            while (spriteRenderer.color.a < 1)
            {
                spriteRenderer.color += new Color(0, 0, 0, 10 * Time.deltaTime);

                yield return null;
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    Vector3 NextMovePoint()
    {
        var posX = Random.Range(BottomLeft.x, TopRight.x);
        var posY = Random.Range(BottomLeft.y, TopRight.y);

        return new Vector2(posX, posY);
    }
}

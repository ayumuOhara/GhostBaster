using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    GhostGenerator ghostGenerator;
    ScoreManager scoreManager;

    public bool Hide => spriteRenderer.color.a <= 0.7f;
    public bool isDead => currentHealth < 0;

    // ƒ‰ƒ“ƒ_ƒ€‚ÅÝ’è‚·‚éHP‚ÌãŒÀ‚Æ‰ºŒÀ
    const float MIN_HEALTH = 3;
    const float MAX_HEALTH = 5;

    [SerializeField] Canvas ghostCanvas;
    [SerializeField] Image healthCircle;
    [SerializeField] SpriteRenderer spriteRenderer;

    Vector2 BottomLeft;
    Vector2 TopRight;

    float currentHealth;
    float maxHealth;

    float moveSpeed = 5.0f;
    float hideSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ghostCanvas.enabled = false;

        ghostGenerator = GameObject.Find("GhostGenerator").GetComponent<GhostGenerator>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        StartCoroutine(Movement());

        maxHealth = Random.Range(MIN_HEALTH, MAX_HEALTH);
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        if (Hide)
        {
            return;
        }
        else
        {
            currentHealth -= Time.deltaTime;
            healthCircle.fillAmount = currentHealth / maxHealth;
        }

        if(isDead) StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        ghostGenerator.RemoveGhost(this);
        scoreManager.IncreaseScore((int)maxHealth * 100);

        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = AlphaSetter.FadeOut(spriteRenderer.color, hideSpeed);
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator Movement()
    {
        Vector3 targetPos = Vector2.zero;
        Vector3 moveDir = Vector2.zero;

        while (true)
        {
            targetPos = RandomPos();
            moveDir = (targetPos - transform.position).normalized;

            while (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;

                spriteRenderer.color = AlphaSetter.FadeOut(spriteRenderer.color, hideSpeed);

                yield return null;
            }

            yield return new WaitForSeconds(1.0f);

            while (spriteRenderer.color.a < 1)
            {
                spriteRenderer.color = AlphaSetter.FadeIn(spriteRenderer.color, hideSpeed);

                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        }
    }

    Vector3 RandomPos()
    {
        var posX = Random.Range(BottomLeft.x, TopRight.x);
        var posY = Random.Range(BottomLeft.y, TopRight.y);

        return new Vector2(posX, posY);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            if (Hide)
            {
                ghostCanvas.enabled = false;
                return;
            }
            else
            {
                TakeDamage();

                ghostCanvas.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ghostCanvas.enabled = false;
    }
}

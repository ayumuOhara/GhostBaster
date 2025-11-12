using UnityEngine;

public class LightController : MonoBehaviour
{
    const float DEFAULT_SCALE = 2.0f;

    [Header("ƒ‰ƒCƒg‚ÌŠgk‚ÌÅ‘å’l")]
    [SerializeField] float maxLightRange;

    [Header("ƒ‰ƒCƒg‚ÌŠgk‚ÌÅ¬’l")]
    [SerializeField] float minLightRange;

    float lightRange = 0;
    public float CurrentLightRange => lightRange;
    public float LightRangeRatio => lightRange / DEFAULT_SCALE;

    [Header("ƒ‰ƒCƒg‚ÌŠgk‚Ì•Ï‰»‘¬“x")]
    [SerializeField] float zoomSpd;

    [Header("ƒ‰ƒCƒg‚ÌŠgk‚Ì•Ï‰»—Ê")]
    [SerializeField] float zoomValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetLightRange();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LightRangeFiddle();
    }

    void Move()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    void LightRangeFiddle()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            DecreaseLightRange();
            SetLightScale();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            IncreaseLightRange();
            SetLightScale();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ResetLightRange();
            SetLightScale();
        }
    }

    void IncreaseLightRange()
    {
        lightRange += zoomValue * zoomSpd * Time.deltaTime;
        lightRange = SaveLightRange();
    }

    void DecreaseLightRange()
    {
        lightRange -= zoomValue * zoomSpd * Time.deltaTime;
        lightRange = SaveLightRange();
    }

    void ResetLightRange()
    {
        lightRange = DEFAULT_SCALE;
        transform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
    }

    float SaveLightRange()
    {
        return Mathf.Clamp(lightRange, minLightRange, maxLightRange);
    }

    void SetLightScale()
    {
        transform.localScale = new Vector3(lightRange, lightRange, lightRange);
    }
}

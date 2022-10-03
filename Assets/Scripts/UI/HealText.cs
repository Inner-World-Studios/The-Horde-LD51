using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup)), RequireComponent(typeof(TextMeshProUGUI))]
public class HealText : PoolableObject
{
    public enum Direction : sbyte
    {
        UP = 1,
        DOWN = -1
    }

    public float textSpeed = 1f;

    public Direction direction = Direction.UP;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI text;
    public Color color;
    public Vector3 offset;

    private GameObject owner;
    private float y;

    private bool isOwnerUI;
    

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDisable)
        {
            canvasGroup.alpha -= 1f * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                Disable();
            }

            float deltaTextSpeed = textSpeed * Time.deltaTime * 1000;
            y += deltaTextSpeed;
            Debug.Log(y);
            if (!isOwnerUI && owner != null)
            {
                transform.position = Camera.main.WorldToScreenPoint(owner.transform.position);
                transform.position += new Vector3(0, y * (float)direction, 0);
            }
            else
            {
                transform.position += new Vector3(0, deltaTextSpeed * (float)direction, 0);
            }
            transform.position += offset;
        }
    }

    public override void OnCreate()
    {
        base.OnCreate();
        canvasGroup.alpha = 1f;
        y = 0f;
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        isOwnerUI = LayerMask.NameToLayer("UI") == owner.layer;
        if (isOwnerUI)
        {
            transform.position = owner.transform.position;
        }
    }

}

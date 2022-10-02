using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup)), RequireComponent(typeof(TextMeshProUGUI))]
public class HealText : PoolableObject
{
    public float textSpeed = 0.5f;

    public CanvasGroup canvasGroup;
    public TextMeshProUGUI text;
    public Color color;

    private Transform owner;
    private float y;

    

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
        canvasGroup.alpha -= 0.001f;
        if (canvasGroup.alpha <= 0)
        {
            Disable();
        }

        y += textSpeed;
        if (owner != null) {
            transform.position = Camera.main.WorldToScreenPoint(owner.position);
        }
        transform.position += new Vector3(0, y, 0);
    }

    public override void OnCreate()
    {
        canvasGroup.alpha = 1;
        y = 0;
    }

    public void SetOwner(Transform owner)
    {
        this.owner = owner;
    }

}

using UnityEngine;

public class Gate : MonoBehaviour
{
    public float fadeRate;

    private SpriteRenderer sprite;
    private float nextFade = 0f;
    private bool isOpening = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isOpening && Time.time > nextFade)
        {
            Color fading = sprite.color;
            fading.a -= 0.1f;
            sprite.color = fading;

            nextFade += fadeRate;
        }

        if(sprite.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Open()
    {
        nextFade = Time.time + fadeRate;
        isOpening = true;
    }
}

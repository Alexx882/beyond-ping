using UnityEngine;

public class DespawnGameObject : MonoBehaviour
{

    public float destroyDelay;

    private bool _fade;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(StartFading), destroyDelay);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fade)
        {
            var newAlpha = Mathf.Lerp(_spriteRenderer.color.a, 0, Time.deltaTime * 3);
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newAlpha);
            
            if (newAlpha < 0.01f)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    void StartFading()
    {
        _fade = true;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartFading();
        }
    }
}

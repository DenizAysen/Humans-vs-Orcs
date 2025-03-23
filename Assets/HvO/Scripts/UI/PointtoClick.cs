using UnityEngine;

public class PointtoClick : MonoBehaviour
{
    [SerializeField] float duration = 1f;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] AnimationCurve scaleCurve;

    private Vector3 _initialScale;

    private float _timer;

    private float _clampTimer;

    private float _freqTimer;
    private void Start()
    {
        _initialScale = transform.localScale;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _freqTimer += Time.deltaTime;

        _freqTimer %= 1;

        float scaleMultiplier = scaleCurve.Evaluate(_timer);
        transform.localScale = _initialScale * scaleMultiplier;

        if(_timer >= duration * 0.9f)
        {
            float fadeProgress = (_timer - duration * 0.9f) / (duration * .1f);
            spriteRenderer.color = new Color(1, 1, 1, 1 - fadeProgress);
        }

        if ( _timer >= duration)
        {
            Destroy(gameObject );
        }
    }
}

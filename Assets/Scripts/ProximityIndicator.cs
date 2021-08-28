using DG.Tweening;
using UnityEngine;

public class ProximityIndicator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name.Equals("PromixityCircle"))
        //{
        spriteRenderer.enabled = true;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(spriteRenderer.DOFade(0.25f, 0.5f));
        sequence.Append(spriteRenderer.DOFade(0.1f, 0.5f));
        sequence.SetLoops<Sequence>(-1, LoopType.Yoyo);
        //}

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.enabled = false;
    }
}

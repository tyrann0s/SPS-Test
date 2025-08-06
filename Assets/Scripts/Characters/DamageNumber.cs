using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Vector3 startPos;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(float damage)
    {
        startPos = transform.position;
        text.text = damage.ToString();
        float rand = UnityEngine.Random.Range(-.25f, .25f);
        DOTween.Sequence().Append(transform.DOMove(new Vector3(startPos.x + rand, startPos.y + .5f, startPos.z), 1f).SetEase(Ease.OutExpo))
            .OnComplete(DestroyGO);
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }
}

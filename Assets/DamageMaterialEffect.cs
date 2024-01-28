using System.Collections;
using UnityEngine;

public class DamageMaterialEffect : MonoBehaviour
{
    [SerializeField]
    private DamagableObject damagableObject;

    [SerializeField]
    private MeshRenderer meshRenderer;

    private Color initialColor;

    private void Awake()
    {
        initialColor = meshRenderer.material.color;
    }

    private void OnEnable()
    {
        damagableObject.OnDamaged += DamagableObject_OnDamaged;
    }

    private void DamagableObject_OnDamaged(DamagableObject obj)
    {
        meshRenderer.material.color = Color.red;
        StopAllCoroutines();
        StartCoroutine(ReturnToOriginalColorCo());
    }

    private IEnumerator ReturnToOriginalColorCo()
    {
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime;
            meshRenderer.material.color = Color.Lerp(Color.red, initialColor, progress);
            yield return null;
        }
        meshRenderer.material.color = initialColor;
    }

    private void OnDisable()
    {
        damagableObject.OnDamaged -= DamagableObject_OnDamaged;
    }
}

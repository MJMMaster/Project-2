using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    [Header("Visuals")]
    public SpriteRenderer spriteRenderer; // for 2D objects
    public MeshRenderer meshRenderer;     // for 3D objects
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;   // time for one flash
    public int flashCount = 3;            // how many times it flashes

    private Color originalSpriteColor;
    private Material[] originalMaterials;

    private void Awake()
    {
        if (spriteRenderer != null)
            originalSpriteColor = spriteRenderer.color;

        if (meshRenderer != null)
        {
            // Make a copy of materials so we don't modify shared materials
            originalMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                originalMaterials[i] = new Material(meshRenderer.materials[i]);
            }
            meshRenderer.materials = originalMaterials;
        }

        // Subscribe to health changes
        Health health = GetComponent<Health>();
        if (health != null)
            health.OnHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged(float healthPercent)
    {
        if (healthPercent < 1f)
        {
            StopAllCoroutines();
            StartCoroutine(FlashStrobe());
        }
    }

    private IEnumerator FlashStrobe()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // Flash ON
            if (spriteRenderer != null)
                spriteRenderer.color = flashColor;

            if (meshRenderer != null)
            {
                foreach (var mat in meshRenderer.materials)
                    mat.color = flashColor;
            }

            yield return new WaitForSeconds(flashDuration);

            // Flash OFF (reset)
            if (spriteRenderer != null)
                spriteRenderer.color = originalSpriteColor;

            if (meshRenderer != null)
            {
                for (int j = 0; j < meshRenderer.materials.Length; j++)
                    meshRenderer.materials[j].color = originalMaterials[j].color;
            }

            yield return new WaitForSeconds(flashDuration);
        }
    }
}
using UnityEngine;

/// <summary>
/// Interactable crop that can be harvested.
/// When harvested, can spawn items or destroy the crop.
/// </summary>
public class CropInteractable : Interactable
{
    [Header("Crop Settings")]
    [SerializeField] private bool isReadyToHarvest = true;
    [SerializeField] private GameObject harvestItemPrefab;
    [SerializeField] private int harvestAmount = 1;
    [SerializeField] private bool destroyOnHarvest = true;

    [Header("Regrowth Settings")]
    [SerializeField] private bool canRegrow = false;
    [SerializeField] private float regrowTime = 5f;

    [Header("Visual Feedback")]
    [SerializeField] private Sprite unharvestedSprite;
    [SerializeField] private Sprite harvestedSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisual();
    }

    public override string GetPromptText()
    {
        if (!isReadyToHarvest)
        {
            return "[Growing...]";
        }
        return promptText;
    }

    public override void Interact()
    {
        if (!isReadyToHarvest)
        {
            Debug.Log("Crop is not ready to harvest yet!");
            return;
        }

        Harvest();
    }

    private void Harvest()
    {
        Debug.Log($"Harvested {harvestAmount}x crop from {gameObject.name}");

        // Spawn harvest items
        if (harvestItemPrefab != null)
        {
            for (int i = 0; i < harvestAmount; i++)
            {
                Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
                Instantiate(harvestItemPrefab, spawnPos, Quaternion.identity);
            }
        }

        if (destroyOnHarvest)
        {
            Destroy(gameObject);
        }
        else if (canRegrow)
        {
            isReadyToHarvest = false;
            UpdateVisual();
            Invoke(nameof(Regrow), regrowTime);
        }
    }

    private void Regrow()
    {
        isReadyToHarvest = true;
        UpdateVisual();
        Debug.Log($"{gameObject.name} has regrown!");
    }

    private void UpdateVisual()
    {
        if (spriteRenderer == null) return;

        if (isReadyToHarvest && unharvestedSprite != null)
        {
            spriteRenderer.sprite = unharvestedSprite;
        }
        else if (!isReadyToHarvest && harvestedSprite != null)
        {
            spriteRenderer.sprite = harvestedSprite;
        }
    }
}

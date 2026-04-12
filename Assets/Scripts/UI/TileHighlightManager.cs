using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlightManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cellAnchor;
    [SerializeField] private GridLayout worldGrid;
    [SerializeField] private Tilemap highlightTilemap;
    [SerializeField] private TileBase highlightTile;

    [Header("Optional: tilemap used to check valid ground")]
    [SerializeField] private Tilemap groundTilemap;

    [Header("Settings")]
    [SerializeField] private Vector2Int facingDirection = Vector2Int.down;

    private Vector3Int previousCell;
    private bool hasPreviousCell = false;

    private void Update()
    {
        UpdateFacingDirection();
        UpdateHighlight();
    }

    private void UpdateFacingDirection()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            facingDirection = Vector2Int.up;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            facingDirection = Vector2Int.down;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            facingDirection = Vector2Int.left;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            facingDirection = Vector2Int.right;
    }

    private void UpdateHighlight()
    {
        Vector3 worldPosition = cellAnchor != null ? cellAnchor.position : playerTransform.position;

        Vector3Int playerCell = worldGrid.WorldToCell(worldPosition);
        Vector3Int targetCell = playerCell + new Vector3Int(facingDirection.x, facingDirection.y, 0);

        ClearPreviousHighlight();

        if (groundTilemap != null)
        {
            if (!groundTilemap.HasTile(targetCell))
                return;
        }

        highlightTilemap.SetTile(targetCell, highlightTile);

        previousCell = targetCell;
        hasPreviousCell = true;
    }

    private void ClearPreviousHighlight()
    {
        if (!hasPreviousCell) return;

        highlightTilemap.SetTile(previousCell, null);
        hasPreviousCell = false;
    }

    public Vector3Int GetTargetCell()
    {
        Vector3 worldPosition = cellAnchor != null ? cellAnchor.position : playerTransform.position;
        Vector3Int playerCell = worldGrid.WorldToCell(worldPosition);
        return playerCell + new Vector3Int(facingDirection.x, facingDirection.y, 0);
    }
}
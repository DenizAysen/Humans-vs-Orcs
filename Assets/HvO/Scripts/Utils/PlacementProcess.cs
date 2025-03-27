
using UnityEngine;

public class PlacementProcess
{
    private BuildActionSO _buildActionSO;

    private GameObject _placementOutline;
    public PlacementProcess(BuildActionSO buildActionSO)
    {
        _buildActionSO = buildActionSO;
    }
    public void Update()
    {
        if(HvoUtils.TryGetHoldPosition(out Vector3 worldPos))
        {
            _placementOutline.transform.position = SnaptoGrid(worldPos);
        }
    }
    public void ShowPLacementOutline()
    {
        _placementOutline = new GameObject("Placement Outline");
        var renderer = _placementOutline.AddComponent<SpriteRenderer>();
        renderer.sortingOrder = 99;
        renderer.color = new Color(1, 1, 1, .5f);
        renderer.sprite = _buildActionSO.PlacementSprite;
    }
    public Vector3 SnaptoGrid(Vector3 worldPos)
    {
        return new Vector3(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y),0);
    }
}

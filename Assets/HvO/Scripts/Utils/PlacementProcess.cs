
using UnityEngine;

public class PlacementProcess
{
    private BuildActionSO _buildActionSO;
    public PlacementProcess(BuildActionSO buildActionSO)
    {
        _buildActionSO = buildActionSO;
    }
    public void Update()
    {
        Debug.Log("Placement Process Update");
    }
    public void ShowPLacementOutline()
    {
        Debug.Log("Show Placament Outline");
    }
}

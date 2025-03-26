using UnityEngine;

[CreateAssetMenu(fileName ="Build Action", menuName = "HvO/Actions/BuildAction")]
public class BuildActionSO : ActionSO
{
    [SerializeField] Sprite placementSprite;
    [SerializeField] Sprite foundationSprite;
    [SerializeField] Sprite completionSprite;

    [SerializeField] int goldCost;
    [SerializeField] int woodCost;

    public Sprite PlacementSprite => placementSprite;
    public Sprite FoundationSprite => foundationSprite;
    public Sprite CompletionSprite => completionSprite;
    public int GoldCost => goldCost;
    public int WoodCost => woodCost;
    public override void Execute(GameManagger gameManagger)
    {
        gameManagger.StartBuildProcess(this);
    }
}

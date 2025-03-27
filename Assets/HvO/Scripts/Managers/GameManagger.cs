using UnityEngine;
using UnityEngine.EventSystems;

public class GameManagger : SingletonManagger<GameManagger>
{
    [Header("UI")]
    [SerializeField] PointtoClick pointtoClickPrefab;
    [SerializeField] ActionBar actionBar;

    public Unit ActiveUnit;

    private PlacementProcess _placementProcess;

    private Vector2 _worldPos;

    private Camera _mainCamera;

    public bool HasActiveUnit => ActiveUnit != null;
    private void Start()
    {
        _mainCamera = Camera.main;
        ClearActionBarUI();
    }
    private void Update()
    {
        if(_placementProcess != null)
        {
            _placementProcess.Update();
        }
        else if (HvoUtils.TryGetShortClickPosition(out Vector2 _worldPos))
        {
            DetectClick(_worldPos);
        }
       
    }
    public void StartBuildProcess(BuildActionSO buildActionSO)
    {
        _placementProcess = new PlacementProcess(buildActionSO);
        _placementProcess.ShowPLacementOutline();
    }
    void DetectClick(Vector2 inputPosition)
    {
        if(HvoUtils.IsPointerOverUIElement())
            return;

        _worldPos = _mainCamera.ScreenToWorldPoint(inputPosition);
        RaycastHit2D hit = Physics2D.Raycast(_worldPos, Vector2.zero);

        if(HasClickedOnUnit(hit , out var Unit))
        {
            HandleClickOnUnit(Unit);
        }
        else
        {
            HandleClickOnGround(_worldPos);
        }
    }
    private bool HasClickedOnUnit(RaycastHit2D hit, out Unit unit)
    {
        if(hit.collider != null && hit.collider.TryGetComponent<Unit>(out var clickedUnit))
        {
            unit = clickedUnit;
            return true;
        }
        unit = null;
        return false;   
    }
    void HandleClickOnGround(Vector2 worldPoint)
    {
        if(HasActiveUnit && IsHumanoid(ActiveUnit))
        {
            ActiveUnit.MoveTo(worldPoint);
            DisplayClickEffect(worldPoint);
        }      
    }
    void HandleClickOnUnit(Unit unit)
    {
        if (HasActiveUnit)
        {
            if (HasClickedOnActiveUnit(unit))
            {
                CancelActiveUnit();
                return;
            }
        }
        SelectNewUnit(unit);

    }
    void SelectNewUnit(Unit unit)
    {
        if(HasActiveUnit)
        {
            ActiveUnit.DeSelect();
        }
        ActiveUnit = unit;
        ActiveUnit.Select();
        ShowUnitActions(unit);
    }
    bool HasClickedOnActiveUnit(Unit clickedUnit)
    {
        return clickedUnit == ActiveUnit;
    }
    bool IsHumanoid(Unit unit)
    {
        return unit is HumanoidUnit;
    }
    void CancelActiveUnit()
    {
        ActiveUnit.DeSelect();
        ActiveUnit = null;


        ClearActionBarUI();
    }
    void DisplayClickEffect(Vector2 worldPoint)
    {
        Instantiate(pointtoClickPrefab, (Vector3)worldPoint, Quaternion.identity);
    }

    void ShowUnitActions(Unit unit)
    {
        ClearActionBarUI();

        if(unit.Actions.Length == 0)
        {
            return;
        }

        actionBar.Show();

        foreach(var action in unit.Actions)
        {
            actionBar.RegisterAction(action.Icon,
                () => action.Execute(this));
        }
    }
    void ClearActionBarUI()
    {
        actionBar.ClearActions();
        actionBar.Hide();
    }
}

using UnityEngine;

public class GameManagger : SingletonManagger<GameManagger>
{
    public Unit ActiveUnit;

    private Vector2 _initialTouchPosition;

    private Vector2 _worldPos;

    private Camera _mainCamera;

    public bool HasActiveUnit => ActiveUnit != null;
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        Vector2 inputPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            _initialTouchPosition = inputPosition;
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if(Vector2.Distance(_initialTouchPosition, inputPosition) < 10)
            {
                DetectClick(inputPosition);
            }

        }
    }

    void DetectClick(Vector2 inputPosition)
    {
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
        ActiveUnit.MoveTo(worldPoint);
    }
    void HandleClickOnUnit(Unit unit)
    {
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
    }

}


using UnityEngine;
using UnityEngine.EventSystems;

public static class HvoUtils 
{
    private static Vector2 _initialTouchPosition;
    public static Vector2 InputPositon => Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;
    public static bool IsLeftClickOrTapDown => Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
    public static bool IsLeftClickOrTapUp => Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended);
    public static bool TryGetShortClickPosition(out Vector2 inputPosition , float maxDistance = 5f)
    {
        inputPosition = InputPositon;

        if (IsLeftClickOrTapDown)
        {
            _initialTouchPosition = InputPositon;
        }

        if (IsLeftClickOrTapUp)
        {
            if (Vector2.Distance(_initialTouchPosition, inputPosition) < maxDistance)
            {
                return true;
            }
        }

        return false;
    }
    public static bool TryGetHoldPosition(out Vector3 worldPos)
    {
        worldPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            worldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            return true;
        }
        else if (Input.GetMouseButton(0))
        {
            worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return true;
        }

        return false;
    }

    public static bool IsPointerOverUIElement()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }
        else
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}

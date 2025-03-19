using UnityEngine;

public class GameManagger : SingletonManagger<GameManagger>
{
    private Vector2 _initialTouchPosition;
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
        Debug.Log(inputPosition);
    }
}

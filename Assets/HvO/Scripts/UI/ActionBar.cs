using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    [SerializeField] Image backgroundImage;

    [SerializeField] ActionButton actionButtonPrefab;

    private Color _originalBackgroundColor;

    private List<ActionButton> _actionButtons = new List<ActionButton>();
    private void Awake()
    {
        _originalBackgroundColor = backgroundImage.color;
    }
    public void RegisterAction()
    {
        var actionButton = Instantiate(actionButtonPrefab, transform);
        _actionButtons.Add(actionButton);
    }
    public void ClearActions()
    {
        for (int i = _actionButtons.Count-1; i >= 0 ; i--)
        {
            Destroy(_actionButtons[i].gameObject);
            _actionButtons.RemoveAt(i);
        }
    }
    public void Show()
    {
        backgroundImage.color = _originalBackgroundColor;
    }
    public void Hide()
    {
        backgroundImage.color = new Color(0, 0, 0, 0);
    }
}

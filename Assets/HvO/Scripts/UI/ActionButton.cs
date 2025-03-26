using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] Button button;

    public void Init(Sprite icon, UnityAction action)
    {
        iconImage.sprite = icon;
        button.onClick.AddListener(action);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}

using UnityEngine;

public class GameManagger : MonoBehaviour
{
    protected virtual void Awake()
    {
        GameManagger[] managers = FindObjectsByType<GameManagger>(FindObjectsSortMode.None);
        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    public static GameManagger Get()
    {
        var tag = nameof(GameManagger);
        GameObject managerObject = GameObject.FindWithTag(tag);
        if (managerObject != null)
        {
            return managerObject.GetComponent<GameManagger>();
        }

        GameObject go = new(tag);
        go.tag = tag;
        return go.AddComponent<GameManagger>();
    }
}

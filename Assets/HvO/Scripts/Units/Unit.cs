using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] ActionSO[] actions;

    public bool IsMoving;

    public bool IsTargeted;

    protected Animator m_Animator;

    protected AIPawn m_AIPawn;

    protected SpriteRenderer m_SpriteRenderer;

    protected Material _originalMaterial;

    protected Material _highlightMaterial;

    public ActionSO[] Actions => actions;
    protected void Awake()
    {
        m_Animator = GetComponent<Animator>();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        if(TryGetComponent<Animator>(out var animator))
        {
            m_Animator = animator;
        }

        if(TryGetComponent<AIPawn>(out var aiPawn))
        {
            m_AIPawn = aiPawn;
        }

        _originalMaterial = m_SpriteRenderer.material;
        _highlightMaterial = Resources.Load<Material>("Materials/Outline");
    }

    public void MoveTo(Vector3 destination)
    {
        var direction = (destination - transform.position).normalized;

        m_SpriteRenderer.flipX = direction.x < 0 ? true : false;

        m_AIPawn.SetDestination(destination);
    }

    public void Select()
    {
        Highlight();
        IsTargeted = true;
    }
    public void DeSelect()
    {
        UnHighlight();
        IsTargeted = false;
    }
    private void Highlight()
    {
        m_SpriteRenderer.material = _highlightMaterial;
    }
    private void UnHighlight()
    {
        m_SpriteRenderer.material = _originalMaterial;
    }
}

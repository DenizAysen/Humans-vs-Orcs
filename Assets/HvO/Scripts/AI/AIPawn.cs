using UnityEngine;

public class AIPawn : MonoBehaviour
{
    private Vector3? _m_Destination;

    [SerializeField] float m_Speed = 5f;

    public Vector3? Destination => _m_Destination;
    private void Start()
    {
        //SetDestination(new Vector3(4f, 2f, 0f));
    }
    private void Update()
    {
        if(_m_Destination.HasValue)
        {
            var direction = (_m_Destination.Value - transform.position).normalized;
            transform.position += direction * Time.deltaTime * m_Speed;

            var distanceToDestination = Vector3.Distance(transform.position,_m_Destination.Value);
            if(distanceToDestination < 0.1f)
            {
                _m_Destination= null;
            }
        }
    }
    public void SetDestination(Vector3? destination) => _m_Destination = destination;

}

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform destination;
    private NavMeshAgent _navmeshagent;

    private void Awake()
    {
        _navmeshagent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _navmeshagent.destination = destination.position;
    }

}

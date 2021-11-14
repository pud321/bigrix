using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator m_animator = null;
    private NavMeshAgent nav_mesh = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        nav_mesh = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate() 
    {
        m_animator.SetBool("Attack", false);
        m_animator.SetFloat("MoveSpeed", nav_mesh.velocity.magnitude);
    }

    public void RunDiscreteAnimation(string command_string)
    {
        m_animator.SetBool(command_string, true);
    }

}

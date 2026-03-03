using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    public float baseSpeed = 3.5f;
    // how much speed changes per lit lamp
    public float speedPerLight = 0.2f; 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // read lights *now* (so it changes during play)
        int lights = GameHandler.Instance != null ? GameHandler.Instance.LitCount : 0;

        // apply speed (clamp so it never goes negative)
        navMeshAgent.speed = Mathf.Max(0.1f, baseSpeed - lights * speedPerLight);
       //check speed is actually chaning
       Debug.Log("Ghost speed: " + navMeshAgent.speed + 
          " | Lights: " + lights);
        if (player != null)
            navMeshAgent.SetDestination(player.position);
    }
}
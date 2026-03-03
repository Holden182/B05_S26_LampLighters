using UnityEngine;

public class HeartbeatDetermine : MonoBehaviour
{
    
    public Transform Ghost;
    public GameObject HeartbeatEffect;
    public Animator heartbeatAnim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Ghost.position);
        heartbeatAnim.SetFloat("Distance", distance);

        if (distance <= 8)
        {

            Debug.Log("Enemy is really close!");
            HeartbeatEffect.SetActive(true);
        }

        else if (distance <= 12)
        {
            Debug.Log("Enemy is close!");
            HeartbeatEffect.SetActive(true);
        }

        else
            HeartbeatEffect.SetActive(false);
    }
}

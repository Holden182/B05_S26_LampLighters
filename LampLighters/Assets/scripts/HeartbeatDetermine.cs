using UnityEngine;

public class HeartbeatDetermine : MonoBehaviour
{
    
    public Transform Ghost;
    public GameObject HeartbeatEffect;
    public Animator heartbeatAnim;
    public AudioSource SoftHB;
    public AudioSource HardHB;
    
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
            HeartbeatEffect.SetActive(true);

            SoftHB.Stop();
            HardHB.Play();

            Debug.Log("Enemy is really close!");
        }

        else if (distance <= 12)
        {
            HeartbeatEffect.SetActive(true);
            
            HardHB.Stop();
            SoftHB.Play();

            Debug.Log("Enemy is close!");
        }

        else
            HeartbeatEffect.SetActive(false);
            SoftHB.Stop();
            HardHB.Stop();
    }
}

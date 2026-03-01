using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 horizVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float speed = horizVel.magnitude;

        bool isWalking = speed > 0.1f;
        anim.SetBool("walk", isWalking);
    }

    public void LightLamp()
    {
        anim.SetTrigger("light");
    }
}
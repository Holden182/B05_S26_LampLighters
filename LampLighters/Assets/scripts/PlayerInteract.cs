using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera cam;
    public float interactRange = 4f;
    public LayerMask interactMask;

    private PlayerAnimation anim;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        anim = GetComponentInParent<PlayerAnimation>(); // or GetComponent<PlayerAnimation>()
    }

    void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactRange, Color.green);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");

            if (Physics.Raycast(cam.transform.position, cam.transform.forward,
                                out RaycastHit hit, interactRange, interactMask))
                                
            {
                Debug.Log("Hit: " + hit.collider.name);

                var lamp = hit.collider.GetComponentInParent<LampToggle>();
                Debug.Log("LampToggle found? " + (lamp != null));

                if (lamp != null)
                {
                    lamp.Toggle();
                    if (anim != null) anim.LightLamp();
                }
            }
            else
            {
                Debug.Log("No hit (mask/range/collider issue)");
            }
        }
    }
}


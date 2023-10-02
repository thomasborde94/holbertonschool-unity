using UnityEngine;

public class bapt : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 0;

    public Vector3 boxSize;
    public Vector3 direction;
    public float maxDistance;
    public LayerMask layerMask;
    public Transform mainCamera;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontale = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = vertical * camForward;
        Vector3 rightRelative = horizontale * camRight;

        Vector3 movDir = forwardRelative + rightRelative;
        movDir.Normalize();

        rb.velocity = new Vector3(movDir.x, rb.velocity.y, movDir.z) * speed;
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Approximately(rb.velocity.y, 0))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            Vector3 jump = Vector3.up * jumpForce;
            rb.AddForce(jump, ForceMode.Impulse);
        }

        //Debug.Log("rb X velocity = " + rb.velocity.x);
        Debug.Log("forward : " + forwardRelative);
        Debug.Log("right " + rightRelative);
        Debug.Log("mov dir : " + movDir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }


}

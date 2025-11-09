using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public Transform cameraTransform;
    public Animator animator; 

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");   

        if (isGrounded)
        {
            animator.SetFloat("moveX", x);
            animator.SetFloat("moveZ", z);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveZ", 0);
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // --- INICIO DEL CAMBIO ---
        // En lugar de "Jump", usamos la tecla física "Space"
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.LogError("Trigger 'Jump' DISPARADO.");
            animator.SetTrigger("Jump");
        }
        // --- FIN DEL CAMBIO ---

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
using UnityEngine;

public class PlayerAnimationTest : MonoBehaviour
{
    // Arrastra tu componente Animator aquí en el Inspector
    public Animator animator;

    // Update se llama una vez por frame
    void Update()
    {
        // 1. Comprueba si la tecla "W" (o la flecha arriba) está presionada
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // 2. Si está presionada, le dice al Animator que ponga "isWalking" en "true"
            animator.SetBool("isWalking", true);
        }
        else
        {
            // 3. Si no está presionada, le dice al Animator que "isWalking" es "false"
            animator.SetBool("isWalking", false);
        }
    }
}
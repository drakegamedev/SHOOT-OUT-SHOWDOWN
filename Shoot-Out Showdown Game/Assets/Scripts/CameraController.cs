using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;                                                  // Animator Component Reference

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Provides a Shaking Effect to the Camera
    /// </summary>
    public void CameraShake()
    {
        animator.SetTrigger("shake");
    }
}

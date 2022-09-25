using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;                                                  // Animator Component Reference

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    #endregion

    #region Public Functions
    public void CameraShake()
    {
        animator.SetTrigger("shake");
    }
    #endregion
}

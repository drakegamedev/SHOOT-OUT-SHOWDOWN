using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public int PlayerNumber;
    public Rigidbody2D Rb { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public Animator Animator { get; private set; }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        //GameManager.Instance.PlayerList.Remove(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlayerList.Add(gameObject);
        Rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();
    }

    
}

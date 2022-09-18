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
    public Gun Gun;

    // Start is called before the first frame update
    void Start()
    {
        // Add Player to the Player List
        GameManager.Instance.PlayerList.Add(gameObject);
        
        // Cache in Class References and Components
        Rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();

        // Set Position
        transform.position = GameManager.Instance.PlayerSpawnPoints[PlayerNumber - 1].position;
    }
}

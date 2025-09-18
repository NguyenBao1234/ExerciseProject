using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarControl : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector3 velocity;
    public float speed = 5;
    private float currentSpeed;
    private CharacterController controller;
    private Coroutine speedCoroutine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }
    
    void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput == null) return;
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
    }
    private void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

    private void HandleMove()
    {
        //if (moveInput == Vector2.zero) return;
        Vector3 inputDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;
        velocity.x = inputDir.x * Math.Min(currentSpeed, 10);
        velocity.z = 1 * currentSpeed;
        velocity.y = -1* currentSpeed;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "VatCan")
        {
			other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (currentSpeed >= speed * 3) currentSpeed = speed;
            else currentSpeed = math.max(speed / 2, currentSpeed - speed / 2);
            if(speedCoroutine != null) StopCoroutine(speedCoroutine);
            StartCoroutine(SetNormalSpeed());
        }
        else if (other.gameObject.name == "Gas")
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            currentSpeed += speed;
            if(speedCoroutine != null) StopCoroutine(speedCoroutine);
            speedCoroutine = StartCoroutine(SetNormalSpeed());
        }
    }
    
    IEnumerator SetNormalSpeed()
    {
        yield return new WaitForSeconds(5);
        currentSpeed = Math.Max( speed, currentSpeed - speed / 2);
    }
}

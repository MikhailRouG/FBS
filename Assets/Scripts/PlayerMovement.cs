using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _runSpeed;
    private float _currentSpeed;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = _normalSpeed;
    }

    public void Move(Vector3 velocity, bool shift)
    {
        if (velocity.x == 0f) rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
        if (velocity.z == 0f) rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0f);
        Vector3 move = transform.right * velocity.x + transform.forward * velocity.z;
        if (shift) _currentSpeed = _runSpeed;
        else _currentSpeed = _normalSpeed;
        rigidbody.velocity = move * _currentSpeed * Time.deltaTime;
    }
}
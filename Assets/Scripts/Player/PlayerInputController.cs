using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2=UnityEngine.Vector2;
using Vector3=UnityEngine.Vector3;

public class PlayerInputController : MonoBehaviour {
    private int _moveSpeed = 10;
    private CharacterController _controller;
    // This method handles input for simple player movement

    public void Awake () {
        _controller = gameObject.AddComponent<CharacterController>();
    }
    
    public void OnMove (InputValue input) {
        Vector2 inputVector = input.Get<Vector2>();
        var move = new Vector3(inputVector.x , 0, inputVector.y);
        _controller.Move(move * Time.deltaTime * _moveSpeed);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

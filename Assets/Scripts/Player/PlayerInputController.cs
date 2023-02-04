using UnityEngine;
using UnityEngine.InputSystem;
using Vector2=UnityEngine.Vector2;

namespace Player {
    public class PlayerInputController : MonoBehaviour {
        private const int MoveSpeed = 10;
        private CharacterController _controller;
        
        private PlayerInput _playerInput;
        private InputAction _playerMove;

        public void Awake () {
            _controller = gameObject.AddComponent<CharacterController>();
            
            _playerInput = gameObject.GetComponent<PlayerInput>();
            _playerMove = _playerInput.actions["Move"];
        }
        
        void Update() {
            var move = _playerMove.ReadValue<Vector2>();
            Debug.Log(move);
            _controller.Move(move * (Time.deltaTime * MoveSpeed));
        }
    }
}

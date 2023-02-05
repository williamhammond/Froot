using UnityEngine;
using UnityEngine.InputSystem;
using Vector2=UnityEngine.Vector2;

namespace Player {
    public class PlayerInputController : MonoBehaviour {
        [SerializeField] Camera mainCam;

        private const int MoveSpeed = 5;
        private CharacterController _controller;
    
        private PlayerInput _playerInput;
        private InputAction _playerMove;
        
        private InputAction _playerAction;

        Vector3 mousePos;
        Plane originPlane;

        public void Awake () {
            originPlane = new Plane(Vector3.up, Vector3.zero);

            _controller = gameObject.AddComponent<CharacterController>();

            _playerInput = gameObject.GetComponent<PlayerInput>();
            _playerMove = _playerInput.actions["Move"];
        }

        public void OnEnable () {
            _playerAction = _playerInput.actions["Action"];
            _playerAction.performed += HandlePlayerAction;
        }
        
        public void OnDisable () {
            _playerAction.performed -= HandlePlayerAction;
        }

        void Update() {
            HandleMovement();
            var ray = new Ray(transform.position, transform.forward);
            Debug.DrawLine(ray.origin, ray.direction * 2 , Color.red);

            originPlane = new Plane(Vector3.up, transform.position);
            var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (originPlane.Raycast(mouseRay, out float hit))
            {
                mousePos = mouseRay.GetPoint(hit);
            }

        }

        private void HandlePlayerAction (InputAction.CallbackContext ctx) {
            float maxRayDistance = 2f;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Ray ray = new Ray(transform.position, forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxRayDistance, LayerMask.GetMask("Default")))
            {
                Debug.Log("Object detected directly in front of player: " + hitInfo.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Nothing detected directly in front of player");
            }
        }

        private void HandleMovement () {
            var input = _playerMove.ReadValue<Vector2>();
            var move = new Vector3(input.x, 0, input.y);
            _controller.Move(move * (Time.deltaTime * MoveSpeed));
            transform.Translate(move.normalized * MoveSpeed * Time.deltaTime, Space.World);



            //Rotation
            Vector3 mouseFlattened = new Vector3(mousePos.x, 0, mousePos.z);
            Vector3 positionFlattened = new Vector3(transform.position.x, 0, transform.position.z);
            Quaternion toRotation = Quaternion.LookRotation(mouseFlattened - positionFlattened, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);     
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(mousePos, .2f);
        }
    }
}


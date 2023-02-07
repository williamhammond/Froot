using UnityEngine;
using UnityEngine.InputSystem;
namespace Player {
    public class PlayerInputController : MonoBehaviour {

        private const int MoveSpeed = 5;
        [SerializeField] private Camera mainCam;
        private CharacterController _controller;

        private InputAction _playerAction;

        private PlayerInput _playerInput;
        private InputAction _playerMove;

        private Vector3 mousePos;
        private Plane originPlane;

        public void Awake () {
            originPlane = new Plane(Vector3.up, Vector3.zero);

            _controller = gameObject.AddComponent<CharacterController>();

            _playerInput = gameObject.GetComponent<PlayerInput>();
            _playerMove = _playerInput.actions["Move"];
        }

        private void Update () {
            HandleMovement();
            var ray = new Ray(transform.position, transform.forward);
            Debug.DrawLine(ray.origin, ray.direction * 2, Color.red);

            originPlane = new Plane(Vector3.up, transform.position);
            var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (originPlane.Raycast(mouseRay, out var hit)) {
                mousePos = mouseRay.GetPoint(hit);
            }

        }

        public void OnEnable () {
            _playerAction = _playerInput.actions["Action"];
            _playerAction.performed += HandlePlayerAction;
        }

        public void OnDisable () {
            _playerAction.performed -= HandlePlayerAction;
        }

        private void OnDrawGizmos () {
            Gizmos.DrawWireSphere(mousePos, .2f);
        }

        private void HandlePlayerAction (InputAction.CallbackContext ctx) {
            var maxRayDistance = 2f;
            var forward = transform.TransformDirection(Vector3.forward);
            var ray = new Ray(transform.position, forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxRayDistance, LayerMask.GetMask("Default"))) {
                Debug.Log("Object detected directly in front of player: " + hitInfo.collider.gameObject.name);
            } else {
                Debug.Log("Nothing detected directly in front of player");
            }
        }

        private void HandleMovement () {
            var input = _playerMove.ReadValue<Vector2>();
            var move = new Vector3(input.x, 0, input.y);
            _controller.Move(move * (Time.deltaTime * MoveSpeed));
            transform.Translate(move.normalized * MoveSpeed * Time.deltaTime, Space.World);


            //Rotation
            var mouseFlattened = new Vector3(mousePos.x, 0, mousePos.z);
            var positionFlattened = new Vector3(transform.position.x, 0, transform.position.z);
            var toRotation = Quaternion.LookRotation(mouseFlattened - positionFlattened, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
        }
    }
}
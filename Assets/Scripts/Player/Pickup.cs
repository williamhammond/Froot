using UnityEngine;

public class Pickup : MonoBehaviour {

    private Backpack _backpack;

    private void Awake () {
        _backpack = GetComponent<Backpack>();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Seeds")) {
            _backpack.seeds++;
            Destroy(other);
        }
    }
}

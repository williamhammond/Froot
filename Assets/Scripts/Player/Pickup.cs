using UnityEngine;

public class Pickup : MonoBehaviour {

    private Backpack _backpack;

    private void Awake () {
        _backpack = GetComponent<Backpack>();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Seeds")) {
            Debug.Log("Picked up seed!");
            _backpack.seeds++;
            Destroy(other.gameObject);
        }
    }
}

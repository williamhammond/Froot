using UnityEngine;

public class Pickup : MonoBehaviour {

    private Backpack _backpack;

    private void Awake () {
        _backpack = GetComponent<Backpack>();
    }

    private void OnTriggerEnter (Collider other) {
        /*
        if (other.gameObject.layer == LayerMask.NameToLayer("Seeds")) {
            Debug.Log("Picked up seed!");
            //AkSoundEngine.PostEvent(SoundManager.CollectSoil, gameObject);
            _backpack.SetSeeds(1);
            Destroy(other.gameObject);
        }
        */
        var pickupable = other.gameObject.GetComponent<Pickupable>();
        if (pickupable!=null)
        {
            pickupable.Pickup(this.gameObject);
        }
    }
}

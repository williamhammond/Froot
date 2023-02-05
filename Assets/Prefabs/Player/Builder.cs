using UnityEngine;
using UnityEngine.InputSystem;

public class Builder : MonoBehaviour {
    private Backpack _backpack;
    private void Awake()
    {
        _backpack = GetComponent<Backpack>();
    }
    bool TryBuild(Tile target)
    {
        if (_backpack.seeds <= 0) {
            Debug.Log("Out of seeds!");
            return false;
        }

        //Put cost logic here
        if (target.occupant == null) 
        { 
            _backpack.seeds--;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            Debug.Log("Hit space!");
            Build();
        }
    }
    public void Build()
    {
        Tile targetTile = gameObject.GetComponentInChildren<Targeter>().target;
        Build(targetTile, buildable);
    }

    [SerializeField]
    Buildable buildable;
    void Build(Tile target, Buildable buildable)
    {
        if(TryBuild(target))
        {
            var newBuild = GameObject.Instantiate(buildable);
            newBuild.Place(target);

            //This is arbatrary, maybe the type of buildable should have an energy amount
            int energyGranted = 4;
            target.MakeAreable(energyGranted);
        }
    }
}

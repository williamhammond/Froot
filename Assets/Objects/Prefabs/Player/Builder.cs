using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Builder : MonoBehaviour {
    private Backpack _backpack;

    UnityEvent onFailBuildSeeds, onFailBuildInvalidSpace, onBuildSuceed;

    private void Awake()
    {
        _backpack = GetComponent<Backpack>();
    }
    [SerializeField]
    int buildcost = 1;
    bool TryBuild(Tile target)
    {
        if (!_backpack.HasSeeds(buildcost)) {
            Debug.Log("Out of seeds!");
            onFailBuildSeeds?.Invoke();

            return false;
        }

        if (!target.IsAreable)
        {
            Debug.Log("Not areable land!");
            onFailBuildInvalidSpace?.Invoke();
            return false;
        }

        //Put cost logic here
        if (target.occupant == null) 
        { 
            //AkSoundEngine.PostEvent(SoundManager.CollectSoil, gameObject);
            _backpack.SetSeeds(-1);
            onBuildSuceed?.Invoke();
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
    [SerializeField]
    Targeter targeter;
    public void Build()
    {
        Tile targetTile = targeter.target;
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

        }
    }
}

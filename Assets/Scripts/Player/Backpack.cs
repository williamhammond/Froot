using UnityEngine;
using UnityEngine.Events;

public class Backpack : MonoBehaviour {
    [SerializeField] private int maxSeeds = 10;
    [SerializeField] private UnityEvent<int> seedChange;
    [SerializeField] private UnityEvent seedsDepleted;
    [SerializeField] private UnityEvent seedsMaxed;
    private int seeds = 1;

    private void Awake () {
        SetSeeds(0);
    }
    public void SetSeeds (int add) {
        seeds += add;
        if (seeds > maxSeeds) {
            seeds = maxSeeds;
            seedsMaxed?.Invoke();
        }
        if (seeds < 0) {
            seeds = 0;
            seedsDepleted?.Invoke();
        }
        seedChange?.Invoke(seeds);
    }

    public bool HasSeeds (int count) {
        if (count <= seeds) return true;
        return false;
    }
}
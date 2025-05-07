using UnityEngine;

public enum Animal {
    Gecko,
    Wallaby
}

public class AnimalManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static AnimalManager instance = null;

    // Awake is called even before start 
    // (I think its at the very beginning of runtime)
    private void Awake() {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private Animal currentAnimal;

    public Animal GetCurrentAnimal { get { return currentAnimal; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimal(Animal newAnimal) {
        currentAnimal = newAnimal;

        // TODO: Change player sprite
    }

    public Color GetAnimalColor() {
        return GetAnimalColor(currentAnimal);
    }

    public Color GetAnimalColor(Animal animal) {
        return animal switch {
            Animal.Gecko => Color.green,
            Animal.Wallaby => new Color(0.5f, 0f, 1f, 1f),
            _ => Color.white,
        };
    }

    public int GetMaxJumpCount() {
        return currentAnimal switch {
            Animal.Gecko => 0,
            Animal.Wallaby => 2,
            _ => 1,
        };
    }

    public bool CanWallWalk() {
        return currentAnimal == Animal.Gecko;
    }
}

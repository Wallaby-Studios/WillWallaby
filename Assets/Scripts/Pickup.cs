using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Animal animalToChangeTo;

    private void Start() {
        GetComponent<SpriteRenderer>().material.color = AnimalManager.instance.GetAnimalColor(animalToChangeTo);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            // Change the player's current animal
            AnimalManager.instance.ChangeAnimal(animalToChangeTo);

            // TODO: Remove when animal sprites are added
            collision.gameObject.GetComponent<SpriteRenderer>().material.color = AnimalManager.instance.GetAnimalColor();

            // Destroy the pickup gameobject
            Destroy(gameObject);
        }
    }
}

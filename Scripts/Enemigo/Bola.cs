using UnityEngine;

public class Bola : MonoBehaviour
{
    void Start()
    {
        // Set the layer of the bola GameObject to "Bola"
        gameObject.layer = LayerMask.NameToLayer("Bola");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigo"))
        {
            // Ignore collision with Enemigo object
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
    }
}

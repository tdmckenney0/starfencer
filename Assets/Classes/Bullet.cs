using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public string enemyTagName = "Enemy";
    public int damage = 1;
    public Explosion explosion;

    // if collides with a Ship, tagged as an enemy. 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(enemyTagName)) {
            
            // Spawn Explosion Object
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);

            // Damage the Ship.
            other.gameObject.GetComponent<Ship>().TakeDamage(damage);

            // Destroy this GameObject.
            Destroy();
        }
    }

    // Destroy if moves off screen (miss).
    void OnBecameInvisible()
    {
        Destroy(); 
    }

    // Shortcut to Destroy the Object.
    void Destroy()
    {
        Object.Destroy(gameObject);
    }
}

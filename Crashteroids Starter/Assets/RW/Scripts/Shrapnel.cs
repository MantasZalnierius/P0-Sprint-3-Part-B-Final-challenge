using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    public Spawner spawner;

    private void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
    }

    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * 5);
        if (transform.position.y > 10 || transform.position.y < -10
            || transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            Game.AsteroidDestroyed();
            Destroy(gameObject);
            spawner.asteroids.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        } 
        // Stops the shrapnel from getting stuck on the player.
        else if (collision.gameObject.GetComponent<Ship>() != null)
        {
            spawner.shrapnelPieces.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> asteroids = new List<GameObject>();
    public List<GameObject> shrapnelPieces = new List<GameObject>();

    [SerializeField]
    private GameObject asteroid1;
    [SerializeField]
    private GameObject asteroid2;
    [SerializeField]
    private GameObject asteroid3;
    [SerializeField]
    private GameObject asteroid4;

    [SerializeField]
    private GameObject shrapnel;

    public void BeginSpawning()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.4f);

        SpawnAsteroid();
        StartCoroutine("Spawn");
    }

    public GameObject SpawnAsteroid()
    {
        int random = Random.Range(1, 5);
        GameObject asteroid;
        switch (random)
        {
            case 1:
                asteroid = Instantiate(asteroid1);
                break;
            case 2:
                asteroid = Instantiate(asteroid2);
                break;
            case 3:
                asteroid = Instantiate(asteroid3);
                break;
            case 4:
                asteroid = Instantiate(asteroid4);
                break;
            default:
                asteroid = Instantiate(asteroid1);
                break;
        }

        asteroid.SetActive(true);
        float xPos = Random.Range(-8.0f, 8.0f);

        // Spawn asteroid just above top of screen at a random point along x-axis
        asteroid.transform.position = new Vector3(xPos, 7.35f, 0);

        asteroids.Add(asteroid);

        return asteroid;
    }

    public void ClearAsteroids()
    {
        foreach(GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }

        asteroids.Clear();
    }

    public void StopSpawning()
    {
        StopCoroutine("Spawn");
    }

    public void SpawnShrapnel(Vector3 position, Collider asteroidCollider)
    {
        GameObject[] spawnedPieces = new GameObject[4]; // Creates an array for the shrapnel pieces.
        for (int i = 0; i < 4; i++) // Loops four times to spawn four pieces of shrapnel.
        {
            // Instantiates, sets the position to the asteroid, ignores collisions with the asteroid.
            spawnedPieces[i] = Instantiate(shrapnel);
            spawnedPieces[i].SetActive(true);
            spawnedPieces[i].GetComponent<Shrapnel>().spawner = this;
            spawnedPieces[i].transform.position = position;
            Physics.IgnoreCollision(spawnedPieces[i].GetComponent<Collider>(), asteroidCollider);

            for (int j = 0; j < i; j++) // Ignores collisions with all created shrapnel.
                Physics.IgnoreCollision(spawnedPieces[i].GetComponent<Collider>(), spawnedPieces[j].GetComponent<Collider>());

            shrapnelPieces.Add(spawnedPieces[i]);
        }
    }

    public void ClearShrapnel()
    {
        foreach (GameObject shrapnelPiece in shrapnelPieces)
        {
            Destroy(shrapnelPiece);
        }

        shrapnelPieces.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HealthTest
    {
        private Game game;
        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject = 
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator checkHealth()
        {
            GameObject asteroid = game.GetSpawner().MaxAsteroid();
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(2, asteroid.GetComponent<Asteroid>().health);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ShrapnelTest
    {
        private Game game;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();

            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = asteroid.transform.position;
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator LasersSpawnShrapnel()
        {
            // The laser and asteroid are created in Setup, we just need to check the shrapnel spawned.
            Assert.AreEqual(4, game.GetSpawner().shrapnelPieces.Count);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShrapnelIsClearedOnNewGame()
        {
            // Makes sure there's at least one shrapnel before starting a new game.
            Assert.Greater(game.GetSpawner().shrapnelPieces.Count, 1);
            game.NewGame();
            Assert.AreEqual(0, game.GetSpawner().shrapnelPieces.Count);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ShrapnelMoves()
        {
            // Makes sure there's at least one shrapnel before accessing the first in the list.
            Assert.Greater(game.GetSpawner().shrapnelPieces.Count, 1);
            GameObject shrapnel = game.GetSpawner().shrapnelPieces[0];
            Vector3 position = shrapnel.transform.position;

            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(position, shrapnel.transform.position);
        }

        [UnityTest]
        public IEnumerator ShrapnelDestoysAsteroids()
        {
            // Makes sure there's at least one shrapnel before accessing the first in the list.
            Assert.Greater(game.GetSpawner().shrapnelPieces.Count, 1);
            GameObject shrapnel = game.GetSpawner().shrapnelPieces[0];

            // Spawns an asteroid and makes it collide with the shrapnel.
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            shrapnel.transform.position = asteroid.transform.position;

            // Wait and then check they're both null.
            yield return new WaitForSeconds(0.1f);
            UnityEngine.Assertions.Assert.IsNull(asteroid);
            UnityEngine.Assertions.Assert.IsNull(shrapnel);
        }
    }
}

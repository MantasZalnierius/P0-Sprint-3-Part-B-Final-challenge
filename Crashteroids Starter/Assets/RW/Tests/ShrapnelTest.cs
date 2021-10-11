﻿using System.Collections;
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
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator LasersSpawnShrapnel()
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = asteroid.transform.position;

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(4, game.GetSpawner().shrapnelPieces.Count);
        }

        [UnityTest]
        public IEnumerator ShrapnelIsClearedOnNewGame()
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = asteroid.transform.position;

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(4, game.GetSpawner().shrapnelPieces.Count);

            game.NewGame();

            Assert.AreEqual(0, game.GetSpawner().shrapnelPieces.Count);
        }
    }
}
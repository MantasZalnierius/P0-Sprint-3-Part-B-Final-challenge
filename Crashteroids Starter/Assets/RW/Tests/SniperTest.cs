﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SniperTest
    {
        private Game game;
        private Ship ship;
        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject = 
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
            ship = game.GetShip();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(ship);
            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator checkAsteroidHealth()
        {
            GameObject asteroid = game.GetSpawner().MaxAsteroid();
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(2, asteroid.GetComponent<Asteroid>().health);
        }
        [UnityTest]
        public IEnumerator checkDamage()
        {
            ship.lastShot();
            yield return new WaitForSeconds(0.4f);
            Assert.AreEqual(2, ship.damage);
        }
        [UnityTest]
        public IEnumerator DestroyOneShot()
        {
            GameObject asteroid = game.GetSpawner().MaxAsteroid();
            GameObject laserShot = ship.SpawnSniperLaser();
            ship.lastShot();
            laserShot.transform.position = asteroid.transform.position;
            yield return new WaitForSeconds(0.1f);
            UnityEngine.Assertions.Assert.IsNull(asteroid);
        }

    }
}
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


namespace Tests
{
    public class TripleShootTests
    {
        private Game game;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
              MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
        }

        [UnityTest]
        public IEnumerator tripleShootTest()
        {
            Ship ship = game.GetShip();
            ship.setStartedTime(true);
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(ship.getIsTripleShoot(), true);
        }

        [UnityTest]
        public IEnumerator tripleShootCounterTest()
        {
            Ship ship = game.GetShip();
            ship.setStartedTime(true);
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(ship.getCounterForTripleShoot(), 3);
        }


    }
}

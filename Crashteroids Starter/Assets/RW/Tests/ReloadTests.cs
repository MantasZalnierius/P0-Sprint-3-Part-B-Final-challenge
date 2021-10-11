using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class LaserTest
{
    private Game game;
    private Ship ship;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
          MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Game"));
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
    public IEnumerator TestReloadDelay()
    {
        for (int i = 0; i < 11; ++i)
        {
            Assert.False(ship.isReloading);
            ship.ShootLaser();
            yield return new WaitForSeconds(0.4f);
        }

        Assert.True(ship.isReloading);
        yield return new WaitForSeconds(2.0f);
        Assert.False(ship.isReloading);
    }
}
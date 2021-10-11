using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class TripleShootCooldownTests
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
    public IEnumerator tripleShootCooldownIsOn()
    {
        Ship ship = game.GetShip();
        ship.setStartedTime(true);
        ship.handleTripleShot();
        yield return new WaitForSeconds(1.0f);
        ship.handleTripleShot();
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(ship.getIsCooldown(), true);
    }

    [UnityTest]
    public IEnumerator tripleShootCooldownIsOver()
    {
        Ship ship = game.GetShip();
        ship.setStartedTime(true);
        ship.handleTripleShot();
        yield return new WaitForSeconds(1.0f);
        ship.handleTripleShot();
        yield return new WaitForSeconds(4.0f);
        Assert.AreEqual(ship.getIsCooldown(), false);
    }
}

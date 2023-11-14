using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MainLogic;

public class MakeTestTurn
{
    private const int heroIndex = 0;
    private const int enemyIndex = 1;
    [Test]
    public void MakeTestTurnSimplePasses()
    {
        var turnUpdater = new GameObject().AddComponent<TurnUpdater>();

        var unitPrefab1 = Resources.Load<GameObject>("Prefabs/Hero");
        var unitPrefab2 = Resources.Load<GameObject>("Prefabs/Enemy");
        var tilePrefab = Resources.Load<GameObject>("Prefabs/SquareTile");

        var unit1 = Object.Instantiate(unitPrefab1);
        var unit2 = Object.Instantiate(unitPrefab2);
        var tile1 = Object.Instantiate(tilePrefab);
        var tile2 = Object.Instantiate(tilePrefab);

        unit1.transform.SetParent(tile1.transform);
        unit2.transform.SetParent(tile2.transform);

        var unitsOfTurnList = new GameObject[] { unit1, unit2 };
        var initialHealth = 10;
        var initialDamage = 5;

        var character1 = new TurnUpdater.Character(initialHealth, initialDamage, 0, 0, true, false);
        var character2 = new TurnUpdater.Character(initialHealth, initialDamage, 1, 1, true, false);
        
        var tileData1 = tile1.GetComponent<TileData>();
        var tileDataInfo1 = new TileData.TileDataInfo
        {
            TileColumn = 0,
            TileRow = 0,
            IsHeroPlace = true,
            IsEnemyPlace = false
        };
        tileData1.SetDataInfo(tileDataInfo1);
        var tileData2 = tile2.GetComponent<TileData>();
        var tileDataInfo2 = new TileData.TileDataInfo
        {
            TileColumn = 1,
            TileRow = 1,
            IsHeroPlace = false,
            IsEnemyPlace = true
        };
        tileData2.SetDataInfo(tileDataInfo2);
        
        unitsOfTurnList[heroIndex].GetComponent<UnitsLogic>().SetUnitsParams(initialHealth, initialDamage, true, false);
        unitsOfTurnList[enemyIndex].GetComponent<UnitsLogic>().SetUnitsParams(initialHealth, initialDamage, true, false);
        unitsOfTurnList[heroIndex].GetComponentInParent<TileData>().SetDataInfo(tileDataInfo1);
        unitsOfTurnList[enemyIndex].GetComponentInParent<TileData>().SetDataInfo(tileDataInfo2);
        // Act
        turnUpdater.MakeTurn(unitsOfTurnList);

        // Assert
        // Перевірте, чи змінилися параметри першого персонажа
        Assert.AreEqual(initialHealth + turnUpdater.PlayersBoostList[0][heroIndex], unitsOfTurnList[0].GetComponent<UnitsLogic>().GetUnitsParams().PlayerHealth);
        Assert.AreEqual(initialDamage + turnUpdater.PlayersBoostList[0][enemyIndex], unitsOfTurnList[0].GetComponent<UnitsLogic>().GetUnitsParams().PlayerDamage);

        // Перевірте, чи змінилися параметри другого персонажа
        Assert.AreEqual(initialHealth + turnUpdater.PlayersBoostList[1][heroIndex], unitsOfTurnList[1].GetComponent<UnitsLogic>().GetUnitsParams().PlayerHealth);
        Assert.AreEqual(initialDamage + turnUpdater.PlayersBoostList[1][enemyIndex], unitsOfTurnList[1].GetComponent<UnitsLogic>().GetUnitsParams().PlayerDamage);

    }
}

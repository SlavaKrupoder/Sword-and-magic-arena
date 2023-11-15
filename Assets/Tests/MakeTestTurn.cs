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
        var _turnUpdater = new GameObject().AddComponent<TurnUpdater>();

        var _unitPrefab1 = Resources.Load<GameObject>("Prefabs/Hero");
        var _unitPrefab2 = Resources.Load<GameObject>("Prefabs/Enemy");
        var _tilePrefab = Resources.Load<GameObject>("Prefabs/SquareTile");

        var _unit1 = Object.Instantiate(_unitPrefab1);
        var _unit2 = Object.Instantiate(_unitPrefab2);
        var _tile1 = Object.Instantiate(_tilePrefab);
        var _tile2 = Object.Instantiate(_tilePrefab);

        _unit1.transform.SetParent(_tile1.transform);
        _unit2.transform.SetParent(_tile2.transform);

        var _unitsOfTurnList = new GameObject[] { _unit1, _unit2 };
        var _initialHealth = 10;
        var _initialDamage = 5;

        var _tileData1 = _tile1.GetComponent<TileData>();
        var _tileDataInfo1 = new TileData.TileDataInfo
        {
            TileColumn = 0,
            TileRow = 0,
            IsHeroPlace = true,
            IsEnemyPlace = false
        };
        _tileData1.SetDataInfo(_tileDataInfo1);
        var _tileData2 = _tile2.GetComponent<TileData>();
        var _tileDataInfo2 = new TileData.TileDataInfo
        {
            TileColumn = 1,
            TileRow = 1,
            IsHeroPlace = false,
            IsEnemyPlace = true
        };
        _tileData2.SetDataInfo(_tileDataInfo2);
        
        _unitsOfTurnList[heroIndex].GetComponent<UnitsLogic>().SetUnitsParams(_initialHealth, _initialDamage, true, false);
        _unitsOfTurnList[enemyIndex].GetComponent<UnitsLogic>().SetUnitsParams(_initialHealth, _initialDamage, true, false);
        _unitsOfTurnList[heroIndex].GetComponentInParent<TileData>().SetDataInfo(_tileDataInfo1);
        _unitsOfTurnList[enemyIndex].GetComponentInParent<TileData>().SetDataInfo(_tileDataInfo2);
        // Act
        _turnUpdater.MakeTurn(_unitsOfTurnList);

        // Assert
        Assert.AreEqual(_initialHealth + _turnUpdater.PlayersBoostList[0][heroIndex], _unitsOfTurnList[0].GetComponent<UnitsLogic>().GetUnitsParams().PlayerHealth);
        Assert.AreEqual(_initialDamage + _turnUpdater.PlayersBoostList[0][enemyIndex], _unitsOfTurnList[0].GetComponent<UnitsLogic>().GetUnitsParams().PlayerDamage);

        Assert.AreEqual(_initialHealth + _turnUpdater.PlayersBoostList[1][heroIndex], _unitsOfTurnList[1].GetComponent<UnitsLogic>().GetUnitsParams().PlayerHealth);
        Assert.AreEqual(_initialDamage + _turnUpdater.PlayersBoostList[1][enemyIndex], _unitsOfTurnList[1].GetComponent<UnitsLogic>().GetUnitsParams().PlayerDamage);

    }
}

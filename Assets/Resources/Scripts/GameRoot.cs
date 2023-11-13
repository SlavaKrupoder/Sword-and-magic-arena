using UnityEngine;
using System.Collections.Generic;
using SF = UnityEngine.SerializeField;

public class GameRoot : MonoBehaviour
{
    [SF] TileMapGenerator mapGenerator;
    [SF] TurnUpdater turnUpdater;
    [SF] GameObject[] tilePrefabs;
    [SF] GameObject[] herosPrefabs;
    [SF] GameObject[] enemyPrefabs;
    [SF] int mapHeight = 5;
    [SF] int mapWidth = 5;
    [SF] int herosCount = 5;
    [SF] int enemyCount = 5;

    (List<Vector2Int> unitsCollection, List<UnitsLogic> unitsList) UnitsMapper()
    {
        List<Vector2Int> _unitsCollection = new List<Vector2Int>();
        List<UnitsLogic> _unitsList = new List<UnitsLogic>();

        // need to create Factory
        for (var i = 0; i < herosCount; i++)
        {
            var _randomX = Random.Range(2, mapWidth / 2);
            var _randomY = Random.Range(2, mapHeight);
            _unitsCollection.Add(new Vector2Int(_randomX, _randomY));
            /// init hero // need to add method
             var _hPref = herosPrefabs[Random.Range(0, herosPrefabs.Length)].GetComponent<UnitsLogic>();
            UnitsLogic _curentHero = Instantiate(_hPref).GetComponent<UnitsLogic>();
            var _unitHealth = Random.Range(12, 20);
            var _unitDamage = Random.Range(5, 10);
            _curentHero.SetUnitsParams(_unitHealth, _unitDamage, true, false);
            ////
            _unitsList.Add(_curentHero);
        }
        // need to create Factory
        for (var i = 0; i < enemyCount; i++)
        {
            var _randomX = Random.Range(mapWidth / 2, mapWidth);
            var _randomY = Random.Range(2, mapHeight);
            _unitsCollection.Add(new Vector2Int(_randomX, _randomY));
            /// init hero // need to add method
             var _enPref = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].GetComponent<UnitsLogic>();
            UnitsLogic _curentEnemy = Instantiate(_enPref).GetComponent<UnitsLogic>();
            var _unitHealth = Random.Range(12, 20);
            var _unitDamage = Random.Range(5, 10);
            _curentEnemy.SetUnitsParams(_unitHealth, _unitDamage, false, true);
            ///
            _unitsList.Add(_curentEnemy);
        }

        return (_unitsCollection, _unitsList);
    }

    public void UpdateUnitsInfo(List<TurnUpdater.Character> charactersList)
    {
        var tilesMap = mapGenerator.GettilesMap();
        var charactersListCount = charactersList.Count;

        for (var i = 0; i < charactersListCount; i++)
        {
            var _column = charactersList[i].ColIndex;
            var _row = charactersList[i].RowIndex;
            var _updatedUnit = tilesMap[_column, _row].GetComponentInChildren<UnitsLogic>();
            var _curentUnit = charactersList[i];
            _updatedUnit.SetUnitsParams(_curentUnit.PlayerHealth, _curentUnit.PlayerDamage, _curentUnit.IsHero, _curentUnit.IsEnemy);
        }
    }

    void Start()
    {
        (List<Vector2Int> unitsPositionCollection, List<UnitsLogic> unitsList) = UnitsMapper();
        mapGenerator.GenerateMap(tilePrefabs, mapWidth, mapHeight, unitsPositionCollection, unitsList);
    }
}

using UnityEngine;
using System.Collections.Generic;
using SF = UnityEngine.SerializeField;

public class GameRoot : MonoBehaviour
{
    [SF] TileMapGenerator mapGenerator;
    [SF] GameObject[] tilePrefabs;
    [SF] int mapHeight = 5;
    [SF] int mapWidth = 5;
    [SF] UnitsLogic[] herosPrefabs;
    [SF] UnitsLogic[] enemyPrefabs;
    [SF] int herosCount = 5;
    [SF] int enemyCount = 5;

    (List<Vector2Int> unitsCollection, List<UnitsLogic> unitsList) UnitsMapper()
    {
        List<Vector2Int> _unitsCollection = new List<Vector2Int>();
        List<UnitsLogic> _unitsList = new List<UnitsLogic>();

        // need to create Factory
        for (var i = 0; i < herosCount; i++)
        {
            var _randomX = Random.Range(0, mapWidth / 2); // Left side of the map
            var _randomY = Random.Range(0, mapHeight);
            _unitsCollection.Add(new Vector2Int(_randomX, _randomY));
            /// init hero // need to add method
            var _curentHero = herosPrefabs[Random.Range(0, herosPrefabs.Length)];
            var _unitHealth = Random.Range(12, 20);
            var _unitDamage = Random.Range(5, 10);
            _curentHero.SetUnitsParams(_unitHealth, _unitDamage, true, false);
            ////
            _unitsList.Add(_curentHero);
        }
        // need to create Factory
        for (var i = 0; i < enemyCount; i++)
        {
            var _randomX = Random.Range(mapWidth / 2, mapWidth); // Right side of the map
            var _randomY = Random.Range(0, mapHeight);
            _unitsCollection.Add(new Vector2Int(_randomX, _randomY));
            /// init hero // need to add method
            var _curentEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            var _unitHealth = Random.Range(12, 20);
            var _unitDamage = Random.Range(5, 10);
            _curentEnemy.SetUnitsParams(_unitHealth, _unitDamage, false, true);
            ///
            _unitsList.Add(_curentEnemy);
        }

        return (_unitsCollection, _unitsList);
    }

    public void UpdateUnitsInfo(List<Character> charactersList)
    {
        var tilesMap = mapGenerator.GettilesMap();
        var charactersListCount = charactersList.Count;

        for (var i = 0; i < charactersListCount; i++)
        {
            var _column = charactersList[i].ColIndex;
            var _row = charactersList[i].RowIndex;
            var _updatedUnit = tilesMap[_column, _row].GetComponent<GameObject>();
            var _unitLogicComponent = _updatedUnit.GetComponent<UnitsLogic>();
            var _curentUnit = charactersList[i];
            _unitLogicComponent.SetUnitsParams(_curentUnit.PlayerHealth, _curentUnit.PlayerDamage, _curentUnit.IsHero, _curentUnit.IsEnemy);
        }

    }

    void Start()
    {
        (List<Vector2Int> unitsPositionCollection, List<UnitsLogic> unitsList) = UnitsMapper();
        mapGenerator.GenerateMap(tilePrefabs, mapWidth, mapHeight, unitsPositionCollection, unitsList);
    }
}

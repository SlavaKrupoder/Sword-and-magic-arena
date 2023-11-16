using UnityEngine;
using System.Collections.Generic;
using SF = UnityEngine.SerializeField;
using PoolSpace;

namespace MainLogic
{
    public class GameRoot : MonoBehaviour
    {
        [SF] private TileMapGenerator mapGenerator;
        [SF] private TurnUpdater turnUpdater;
        [SF] private ObjectsPool tilePrefabsPool;
        [SF] private ObjectsPool unitsPrefabPool;
        [SF] private int mapHeight = 5;
        [SF] private int mapWidth = 5;
        [SF] private int herosCount = 5;
        [SF] private int enemyCount = 5;

        public void UpdateUnitsInfo(List<TurnUpdater.Character> charactersList)
        {
            var _tilesMap = mapGenerator.GetTilesMap();
            var _charactersListCount = charactersList.Count;

            for (var i = 0; i < _charactersListCount; i++)
            {
                if (charactersList[i] != null)
                {
                    var _column = charactersList[i].ColIndex;
                    var _row = charactersList[i].RowIndex;
                    var _updatedUnit = _tilesMap[_column, _row].GetComponentInChildren<UnitsLogic>();
                    var _curentUnit = charactersList[i];
                    _updatedUnit.SetUnitsParams(_curentUnit.PlayerHealth, _curentUnit.PlayerDamage, _curentUnit.IsHero, _curentUnit.IsEnemy);
                }
            }
            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            int _herosCount = 0;
            int _enemysCount = 0;
            var _unitsList = mapGenerator.GetTilesMap();

            for (int i = 0; i < _unitsList.GetLength(0); i++)
            {
                for (int j = 0; j < _unitsList.GetLength(1); j++)
                {
                    UnitsLogic _currentObject = _unitsList[i, j]?.GetComponentInChildren<UnitsLogic>();

                    if (_currentObject != null && _currentObject.CompareTag("Enemy"))
                    {
                        _enemysCount++;
                    }
                    else if (_currentObject != null && _currentObject.CompareTag("Hero"))
                    {
                        _herosCount++;
                    }
                }

            }

            if (_herosCount <= 0)
            {
                EventManager.EventManagerObjectLink.SendEndGameEvent("Enemys team win! You Lose :(");
            }
            else if (_enemysCount <= 0)
            {
                EventManager.EventManagerObjectLink.SendEndGameEvent("Heroes team win!");
            }
        }

        void Start()
        {
            unitsPrefabPool.InitPool(herosCount + enemyCount);
            tilePrefabsPool.InitPool(mapWidth * mapHeight);
            mapGenerator.GenerateMap(tilePrefabsPool, mapWidth, mapHeight);

            UnitsMapper _unitsMapper = new UnitsMapper(herosCount, enemyCount, unitsPrefabPool, mapWidth, mapHeight);
            List<UnitsLogic> _unitsList = _unitsMapper.GetInitialUnits();
            List<Vector2Int> _unitsPositionCollection = _unitsMapper.GetInitialUnitsPosition();
            
            GameObject[,] _tilesMap = mapGenerator.GetTilesMap();
            UnitsPositioner _unitsPositioner = new UnitsPositioner(_unitsPositionCollection, _unitsList, _tilesMap);

            var unitList = _unitsPositioner.GetUnitsList();
        }
    }
}
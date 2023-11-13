using UnityEngine;
using System.Collections.Generic;

public class TileMapGenerator : MonoBehaviour
{
    private GameObject[,] tiles;

    public void GenerateMap(GameObject[] tilePrefabs, int mapWidth, int mapHeight, List<Vector2Int> unitsCollection, List<UnitsLogic> unitsList)
    {
        tiles = new GameObject[mapWidth, mapHeight];

        for (var x = 0; x < mapWidth; x++)
        {
            for (var y = 0; y < mapHeight; y++)
            {
                GameObject _tilePrefab = tilePrefabs[UnityEngine.Random.Range(0, tilePrefabs.Length)];
                GameObject _newTile = Instantiate(_tilePrefab, gameObject.transform);
                _newTile.transform.localPosition = new Vector3(x, y, 0);
                var _tileData = _newTile.GetComponent<TileData>();

                if (unitsCollection.Contains(new Vector2Int(x, y)))
                {
                    var _index = unitsCollection.IndexOf(new Vector2Int(x, y));
                    var _unitParams = unitsList[_index].GetUnitsParams();
                    TileData.TileDataInfo _currentTileData = new TileData.TileDataInfo();
                    _currentTileData.TileColumn = x;
                    _currentTileData.TileRow = y;
                    _currentTileData.IsEnemyPlace = _unitParams.IsEnemy;
                    _currentTileData.IsHeroPlace = _unitParams.IsHero;
                   // AddUnit(unitsCollection[y], unitsList[y]);

                    _tileData.SetDataInfo(_currentTileData);
                }
                tiles[x, y] = _newTile;
            }
        }

        for (var i = 0; i < unitsCollection.Count; i++)
        {
            AddUnits(unitsCollection[i], unitsList[i]);
        }
        
    }

    public GameObject[,] GettilesMap()
    {
        return tiles;
    }

    private void AddUnits(Vector2Int position, UnitsLogic unit)
    {
        GameObject _tile = tiles[position.x, position.y];
        GameObject _newUnit = Instantiate(unit.gameObject, _tile.transform);
        UnitsLogic _unitInfo_ = _newUnit.GetComponent<UnitsLogic>();
        var _heroData = unit.GetUnitsParams();
        _unitInfo_.SetUnitsParams(_heroData.PlayerHealth, _heroData.PlayerDamage, _heroData.IsHero, _heroData.IsEnemy);
        _newUnit.transform.position = new Vector3(_tile.transform.position.x, _tile.transform.position.y, 1);
    }

 }

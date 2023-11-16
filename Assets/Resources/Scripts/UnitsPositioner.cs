using MainLogic;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPositioner
{
    private List<GameObject> unitsList = new List<GameObject>();

    public UnitsPositioner(List<Vector2Int> unitsPositionCollection, List<UnitsLogic> unitsList, GameObject[,] tilesMap)
    {
        for (var i = 0; i < unitsPositionCollection.Count; i++)
        {
            var _unitPosition = unitsPositionCollection[i];
            var _unitParams = unitsList[i].GetUnitsParams();
            TileData.TileDataInfo _currentTileData = new TileData.TileDataInfo();
            _currentTileData.TileColumn = _unitPosition.x;
            _currentTileData.TileRow = _unitPosition.y;
            _currentTileData.IsEnemyPlace = _unitParams.IsEnemy;
            _currentTileData.IsHeroPlace = _unitParams.IsHero;
            GameObject _tile = tilesMap[_unitPosition.x, _unitPosition.y];
            TileData _tileData = _tile.GetComponent<TileData>();
            _tileData.SetDataInfo(_currentTileData);
            AddUnits(unitsPositionCollection[i], unitsList[i], _tile);
        }
    }

    public List<GameObject> GetUnitsList()
    {
        return unitsList;
    }

    private void AddUnits(Vector2Int position, UnitsLogic unit, GameObject tile)
    {
        GameObject _unitGameObject = unit.gameObject;
        var _heroData = unit.GetUnitsParams();
       
        if (_unitGameObject != null)
        {
            GameObject _newUnit = unit.gameObject;
            _newUnit.transform.SetParent(tile.transform);
            UnitsLogic _unitInfo_ = _newUnit.GetComponent<UnitsLogic>();

            _unitInfo_.SetUnitsParams(_heroData.PlayerHealth, _heroData.PlayerDamage, _heroData.IsHero, _heroData.IsEnemy);
            _newUnit.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 1);
            unitsList.Add(_newUnit);
        }
    }
}

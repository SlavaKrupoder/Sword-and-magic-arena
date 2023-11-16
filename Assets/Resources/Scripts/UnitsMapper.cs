using PoolSpace;
using System.Collections.Generic;
using UnityEngine;

namespace MainLogic
{
    public class UnitsMapper
    {
        private List<Vector2Int> unitsCollection = new List<Vector2Int>();
        private List<UnitsLogic> initialUnits = new List<UnitsLogic>();
        private static string HeroTag = "Hero";
        private static string EnemyTag = "Enemy";

        public UnitsMapper(int heroCount, int enemyCount, ObjectsPool objectsPool, int mapWidth, int mapHeight)
        {
            var _totalUnitsCount = heroCount + enemyCount;
            var _heroCounter = 0;

            for (var i = 0; i < _totalUnitsCount; i++)
            {
                var _isHero = i < heroCount ? true : false;
                var _unitParams = GetRandomParam(heroCount, enemyCount, _isHero);
                var _curentHero = objectsPool.GetPoolElement();
                UnitsLogic _currentUnitsLogic = _curentHero.GetComponent<UnitsLogic>();
                _curentHero.tag = _isHero ? HeroTag : EnemyTag;
                var _randomX = _isHero ? Random.Range(2, (mapWidth / 2) - 1) : Random.Range((mapWidth / 2) +1, mapWidth);
                var _randomY = Random.Range(2, mapHeight);

                _currentUnitsLogic.SetUnitsParams(_unitParams.UnitHealth, _unitParams.UnitDamage, _isHero, !_isHero);
                initialUnits.Add(_currentUnitsLogic);
                unitsCollection.Add(new Vector2Int(_randomX, _randomY));
                _heroCounter++;
            }
        }

        UnitParam GetRandomParam(int heroCount, int enemyCount, bool isHero)
        {
            var _boostValue = (float)heroCount / enemyCount;

            var _defHpParam = (12, 20);
            var _defDamageParam = (5, 10);

            if (isHero)
            {
                _defHpParam.Item1 += (int)_boostValue;
                _defHpParam.Item2 += (int)_boostValue;

                _defDamageParam.Item1 += (int)_boostValue;
                _defDamageParam.Item2 += (int)_boostValue;
            }

            var _unitHealth = Random.Range(_defHpParam.Item1, _defHpParam.Item2 + 1);
            var _unitDamage = Random.Range(_defDamageParam.Item1, _defDamageParam.Item2 + 1);
            var _params = new UnitParam();
            _params.UnitHealth = _unitHealth;
            _params.UnitDamage = _unitDamage;

            return _params;
        }

        public List<UnitsLogic> GetInitialUnits()
        {
            return initialUnits;
        }

        public List<Vector2Int> GetInitialUnitsPosition()
        {
            return unitsCollection;
        }

        private class UnitParam
        {
            public int UnitHealth { get; set; }
            public int UnitDamage { get; set; }
        }
    }
}


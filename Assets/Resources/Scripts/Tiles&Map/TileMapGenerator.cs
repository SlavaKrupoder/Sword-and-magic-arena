using UnityEngine;
using PoolSpace;

namespace MainLogic
{
    public class TileMapGenerator : MonoBehaviour
    {
        private GameObject[,] tilesMap;

        public void GenerateMap(ObjectsPool tilePrefabsPool, int mapWidth, int mapHeight)
        {
            tilesMap = new GameObject[mapWidth, mapHeight];

            for (var x = 0; x < mapWidth; x++)
            {
                for (var y = 0; y < mapHeight; y++)
                {
                    GameObject _tilePrefab = tilePrefabsPool.GetPoolElement();
                    GameObject _newTile = Instantiate(_tilePrefab, gameObject.transform);
                    _newTile.transform.localPosition = new Vector3(x, y, 0);
                    tilesMap[x, y] = _newTile;
                }
            }
        }

        public GameObject[,] GetTilesMap()
        {
            return tilesMap;
        }
    }
}
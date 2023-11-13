using UnityEngine;

public class TileData : MonoBehaviour
{
    TileDataInfo currentTileData = new TileDataInfo();
   
    public void SetDataInfo(TileDataInfo currentTileData)
    {
        this.currentTileData = currentTileData;

        if (this.currentTileData.IsHeroPlace)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (this.currentTileData.IsEnemyPlace)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public TileDataInfo GetDataInfo()
    {
        return currentTileData;
    }

    public struct TileDataInfo
    {
        public int TileColumn;
        public int TileRow;
        public bool IsHeroPlace;
        public bool IsEnemyPlace;
    }
}

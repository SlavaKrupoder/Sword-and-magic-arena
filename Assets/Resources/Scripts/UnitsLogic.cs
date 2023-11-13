using TMPro;
using UnityEngine;
using SF = UnityEngine.SerializeField;

public class UnitsLogic : MonoBehaviour
{
    [SF] private TextMeshPro HeroHpText;
    [SF] private TextMeshPro HeroDmText;
    private HeroDataInfo HeroData;

    public void SetUnitsParams(int heroHealth, int heroDamage, bool isHero, bool IsEnemy)
    {
        HeroData = new HeroDataInfo();
        HeroData.IsHero = isHero;
        HeroData.IsEnemy = IsEnemy;
        HeroData.PlayerDamage = heroDamage;
        HeroData.PlayerHealth = heroHealth;
        HeroHpText.text = heroHealth.ToString();
        HeroDmText.text = heroDamage.ToString();
        CheckUnitParams();
    }

    public HeroDataInfo GetUnitsParams()
    {
        return HeroData;
    }

    private void CheckUnitParams()
    {
        // need to create obj pool and returnt to pool after dead
        if (HeroData.PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public class HeroDataInfo
    {
        public int PlayerHealth = 0;
        public int PlayerDamage = 0;
        public bool IsHero = false;
        public bool IsEnemy = false;
    }
   
}
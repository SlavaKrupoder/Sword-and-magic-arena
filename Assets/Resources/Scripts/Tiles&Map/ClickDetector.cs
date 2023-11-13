using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] ClickHandler clickHandler;
    private GameObject[] unitsOfTurnList = new GameObject[2];
    private const int HeroSelectableObject = 0;
    private const int EnemySelectableObject = 1;
    private const string HeroTag = "Hero";
    private const string EnemyTag = "Enemy";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log("Clicked on: " + clickedObject.name);
                
                if (unitsOfTurnList[HeroSelectableObject] == null || unitsOfTurnList[EnemySelectableObject] == null)
                {
                    if (clickedObject.CompareTag(HeroTag) && unitsOfTurnList[HeroSelectableObject] == null)
                    {
                        unitsOfTurnList[HeroSelectableObject] = clickedObject;
                        // for test
                        var _spriteRenderer = clickedObject.GetComponent<SpriteRenderer>();
                        Color spriteColor = _spriteRenderer.color;
                        spriteColor.a = 1f;
                        _spriteRenderer.color = spriteColor;
                    }
                    else if (clickedObject.CompareTag(EnemyTag) && unitsOfTurnList[EnemySelectableObject] == null)
                    {
                        unitsOfTurnList[EnemySelectableObject] = clickedObject;
                        // for test
                        var _spriteRenderer = clickedObject.GetComponent<SpriteRenderer>();
                        Color spriteColor = _spriteRenderer.color;
                        spriteColor.a = 1f;
                        _spriteRenderer.color = spriteColor;
                    }
                }

                if(unitsOfTurnList[HeroSelectableObject] != null && unitsOfTurnList[EnemySelectableObject] != null)
                {
                    clickHandler.HandleClick(unitsOfTurnList);
                }
            }
        }
    }

    public void ResetTurnSlots()
    {
        unitsOfTurnList = new GameObject[2];
    }
}

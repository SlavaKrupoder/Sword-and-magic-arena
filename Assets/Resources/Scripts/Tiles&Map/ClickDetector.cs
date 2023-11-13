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
                clickedObject.GetComponent<SpriteRenderer>().color = Color.red;

                if(unitsOfTurnList[HeroSelectableObject] == null || unitsOfTurnList[EnemySelectableObject] == null)
                {
                    if (clickedObject.CompareTag(HeroTag))
                    {
                        unitsOfTurnList[HeroSelectableObject] = clickedObject;
                    }
                    else if (clickedObject.CompareTag(EnemyTag))
                    {
                        unitsOfTurnList[EnemySelectableObject] = clickedObject;
                        clickHandler.HandleClick(unitsOfTurnList);
                    }
                }
            }
        }
    }
}

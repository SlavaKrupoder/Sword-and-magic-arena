using UnityEngine;
using MainLogic;

namespace UiLogic
{
    public class ClickDetector : MonoBehaviour
    {
        [SerializeField] ClickHandler clickHandler;
        [SerializeField] TileMapGenerator tileMapGenerator;
        private GameObject[] unitsOfTurnList = new GameObject[2];
        private bool isPlayerTurn = true;
        private const int HeroSelectableObject = 0;
        private const int EnemySelectableObject = 1;
        private const string HeroTag = "Hero";
        private const string EnemyTag = "Enemy";

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isPlayerTurn == true)
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

                    if (unitsOfTurnList[HeroSelectableObject] != null && unitsOfTurnList[EnemySelectableObject] != null)
                    {
                        clickHandler.HandleClick(unitsOfTurnList);
                        isPlayerTurn = false;
                    }
                }
            }
        }

        public void MakeAITurn()
        {
            var _unitsList = tileMapGenerator.GetTilesMap();
            unitsOfTurnList = new GameObject[2];

            if (isPlayerTurn == false)
            {
                for (int i = 0; i < _unitsList.GetLength(0); i++)
                {
                    for (int j = 0; j < _unitsList.GetLength(1); j++)
                    {
                        UnitsLogic currentObject = _unitsList[i, j]?.GetComponentInChildren<UnitsLogic>();

                        if (currentObject != null && currentObject.CompareTag("Enemy"))
                        {
                            unitsOfTurnList[0] = currentObject.gameObject;
                            break;
                        }
                    }
                    if (unitsOfTurnList[0] != null)
                    {
                        break;
                    }
                }

                for (int i = 0; i < _unitsList.GetLength(0); i++)
                {
                    for (int j = 0; j < _unitsList.GetLength(1); j++)
                    {
                        UnitsLogic currentObject = _unitsList[i, j]?.GetComponentInChildren<UnitsLogic>();

                        if (currentObject != null && currentObject.CompareTag("Hero"))
                        {
                            unitsOfTurnList[1] = currentObject.gameObject;
                            break;
                        }
                    }
                    if (unitsOfTurnList[1] != null)
                    {
                        break;
                    }
                }
                clickHandler.HandleAiClick(unitsOfTurnList);
                isPlayerTurn = true;
            }
        }
    }
}

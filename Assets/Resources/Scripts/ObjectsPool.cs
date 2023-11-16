using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace PoolSpace
{
    public class ObjectsPool : MonoBehaviour
    {
       [SF] private GameObject[] _poolObject;
       [SF] private Stack<GameObject> _objectPoolStack = new Stack<GameObject>();

        public GameObject GetPoolElement()
        {
            GameObject poolObject = null;
            if (!_objectPoolStack.TryPop(out poolObject))
            {
                poolObject = CreateNewPoolObject();
                ReturnObjectToPool(poolObject);
            }
            else
            {
                poolObject.SetActive(true);
            }

            return poolObject;
        }

        public void ReturnObjectToPool(GameObject usedPoolObject)
        {
            if (usedPoolObject)
            {
                ResetPoolObjectParam(usedPoolObject);
                _objectPoolStack.Push(usedPoolObject);
            }
            else
            {
                MethodBase currentMethod = MethodBase.GetCurrentMethod();
                Debug.Log(currentMethod.Name + " Dont have Parameters");
            }
        }

        public void InitPool(int poolLenght)
        {
            var poolChildCount = transform.childCount;
            for (var i = 0; i < poolLenght; i++)
            {
                GameObject newPoolElement = null;
                if (i < poolChildCount)
                {
                    newPoolElement = transform.GetChild(i).gameObject;
                }
                else
                {
                    newPoolElement = CreateNewPoolObject();
                }
                ReturnObjectToPool(newPoolElement);
            }
        }

        private GameObject CreateNewPoolObject()
        {
            var _randObjectIndex = Random.Range(0, _poolObject.Length);

            return Instantiate(_poolObject[_randObjectIndex]);
        }

        private void ResetPoolObjectParam(GameObject newPoolObject)
        {
            if (newPoolObject)
            {
                newPoolObject.transform.SetParent(transform);
                newPoolObject.SetActive(false);
            }
            else
            {
                MethodBase currentMethod = MethodBase.GetCurrentMethod();
                Debug.Log(currentMethod.Name + " No have Parameters");
            }
        }
    }
}

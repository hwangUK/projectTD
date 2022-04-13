using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKH
{
    public class UCustomMono<T> : MonoBehaviour
    {
        private T rootCore;

        protected T RootCore 
        { 
            get
            {
                if(rootCore == null)
                    rootCore = UFindRoot<T>();

                return rootCore;
            }
        }

        virtual protected void Awake()
        {
            if (rootCore == null)
                rootCore = UFindRoot<T>();
        }
        public T GetRootCore()
        {
            return rootCore;
        }

        public Transform GetParent()
        {
           return this.transform.parent.transform;
        }

        public Transform UFindRoot(string objName)
        {
            Transform result = this.transform;
            do
            {
                if (result.Find(objName) != null)
                {
                    return result.Find(objName);
                }
                else
                {
                    for(int i=0; i< result.childCount; i++)
                    {
                        if (result.GetChild(i).Find(objName) != null)
                            return result.GetChild(i).Find(objName);
                    }
                    result = result.parent;
                }
            }
            while (result.parent != null);

            return null;
        }
#pragma warning disable CS0693 // ���� �Ű� ������ �ܺ� ������ ���� �Ű� ������ �̸��� �����ϴ�.
        public T UFindRoot<T>()
#pragma warning restore CS0693 // ���� �Ű� ������ �ܺ� ������ ���� �Ű� ������ �̸��� �����ϴ�.
        {
            //�������� ������Ʈ�� ���� ��쿡�� ����ó���� �������

            Transform result = this.transform;
            do
            {
                if (result.GetComponent<T>() != null)
                {
                    return result.GetComponent<T>();
                }
                else
                {
                    for(int i=0; i< result.childCount; i++)
                    {
                        if(result.GetChild(i).GetComponent<T>() != null)
                        {
                            return result.GetChild(i).GetComponent<T>();
                        }
                    }
                }
                result = result.parent;
            }
            while (result != null);

            Debug.LogError("UFindRoot ��ã�� : " + typeof(T));
            return default;
        }
    }
}
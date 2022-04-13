using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKH
{
    public class USingletoneMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public bool isDone = false;

        protected virtual void Awake()
        {
            InitInstance();
        }

        void InitInstance()
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                isDone = true;
            }
        }
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                }
                return instance;
            }
        }

        private void OnDestroy()
        {
            FinallizeInstance();
        }
        public static void FinallizeInstance()
        {
            instance = null;
        }
    }

    public class USingletoneMonoDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public bool isDone = false;

        protected virtual void Awake()
        {
            InitInstance();
        }

        void InitInstance()
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                DontDestroyOnLoad(this.gameObject);
                isDone = true;
            }
            //else
            //{
            //    Destroy(this.gameObject);
            //    instance = null;
            //    isDone = false;
            //}
        }
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance)
                        DontDestroyOnLoad(instance);
                    else
                        return null;
                }
                return instance;
            }
        }

        private void OnDestroy()
        {
            FinallizeInstance();
        }
        public static void FinallizeInstance()
        {
            instance = null;
        }
    }
}
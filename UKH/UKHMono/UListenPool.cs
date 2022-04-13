using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKH
{
    public abstract class UListenPool : MonoBehaviour
    {
        [Header("랜더그룹")]
        [SerializeField] protected GameObject _renderGroup;
        [Space(10)]

        private int  _uid;
        private bool _isPoolOn;
        public bool IsPoolOn => _isPoolOn;
        public int  Uid =>_uid;

        public abstract void VisibleRenderGroup();
        public abstract void SleepRenderGroup();

        public void AwakePool(int uid)
        {
            _uid = uid;
            _isPoolOn = true;
            this.gameObject.SetActive(true);
        }
        public void SleepPool()
        {
            _uid = -1;
            _isPoolOn = false;
            this.gameObject.SetActive(false);
        }
    }
}
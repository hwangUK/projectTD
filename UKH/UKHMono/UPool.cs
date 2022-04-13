using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUnitPresetType
{
    T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15 
}
namespace UKH
{
    public class UPool : MonoBehaviour
    {
        [SerializeField] private bool isAlive;
        public bool IsAlive { get => isAlive; }
      
        protected void SetAliveFalse()
        {
            //Debug.LogWarning($"{this.gameObject.name} SetActiveFalse");
            isAlive = false;
            this.gameObject.SetActive(false);
        }
        protected void SetAliveTrue()
        {
            //Debug.LogWarning($"{this.gameObject.name} SetActiveTrue");
            this.gameObject.SetActive(true);
            isAlive = true;
        }
    }
}

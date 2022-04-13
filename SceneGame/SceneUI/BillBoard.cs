using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(this.transform.position - Camera.main.transform.position);
    }
}

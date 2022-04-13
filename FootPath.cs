using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPath : MonoBehaviour
{
    Transform[] path;
    [SerializeField] float separation = 2f;
    public Transform[] Path { get => path; set => path = value; }
   
    public void InitInstance()
    {
        path = new Transform[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            path[i] = this.transform.GetChild(i);
        }
    }
    public Vector3 GetTargetPos(int point)
    {        
        return path[point].position;
    }
}

#region prevVer
//public Vector3 GetTargetPos(Transform originPos, OffenceUnit.ELine line, int point)
//{
//    Vector3 dirVec = Vector3.zero;
//
//    if (line == OffenceUnit.ELine.left)
//        dirVec = originPos.TransformDirection(Vector3.left * separation) - originPos.position;
//    if (line == OffenceUnit.ELine.right)
//        dirVec = originPos.TransformDirection(Vector3.right * separation) - originPos.position;
//
//    return path[point].position + dirVec;
//}
// Start is called before the first frame update

//[SerializeField] Transform[] lanes;
//Vector3[,] footPos;
//public Vector3[,] FootPos { get => footPos; set => footPos = value; }
//public void InitInstacne()
//{
//    footPos = new Vector3[lanes.Length, lanes[0].childCount];
//
//    for (int i =0; i< lanes.Length; i++)
//    {
//        for(int j =0; j< lanes[i].childCount; j++)
//        {
//            footPos[i, j] = lanes[i].GetChild(j).position;
//        }
//    }
//}  
#endregion
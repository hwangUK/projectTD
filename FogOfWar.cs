using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour 
{
	[SerializeField] private GameObject	fogOfWarPlane;
	[SerializeField] private Transform	target;
	[SerializeField] private float		radius = 3f;

	[SerializeField] private LayerMask	fogLayer;
	[SerializeField] private Color		fogColor;
	private Mesh		fogMesh;
	private Vector3[]	fogVertices;
	private Color[]		resultColors;
	private float RadiusSqr { get { return radius * radius; } }

	// Use this for initialization
	void Start () 
	{
		Initialize();
	}
	void Initialize()
	{
		fogOfWarPlane.gameObject.SetActive(true);
		fogMesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		fogVertices = fogMesh.vertices;
		resultColors = new Color[fogVertices.Length];
		for (int i = 0; i < resultColors.Length; i++)
		{
			resultColors[i] = fogColor;// new Color(0f / 255f, 12f / 255f, 22f / 255f, 0.7f);// Color.black;
		}
		UpdateColor();
	}

	// Update is called once per frame
	void Update () 
	{
		Ray ray = new Ray(transform.position, target.position - transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, fogLayer, QueryTriggerInteraction.Collide)) 
		{
			for (int i=0; i< fogVertices.Length; i++) 
			{
				Vector3 verticeWorldPos  = fogOfWarPlane.transform.TransformPoint(fogVertices[i]);
				float dist = Vector3.SqrMagnitude(verticeWorldPos - hit.point);
				if (dist < RadiusSqr) 
				{
					float alpha = Mathf.Min(resultColors[i].a, dist/RadiusSqr);
					resultColors[i].a = alpha;
				}
			}
			UpdateColor();
		}
	}
	void UpdateColor() 
	{
		fogMesh.colors = resultColors;
	}
}

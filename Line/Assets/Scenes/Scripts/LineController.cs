using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    //[SerializeField] List<Transform> listPoint;
	private LineRenderer lr;

	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
	}

	public void SetupLine(List<Vector3> list)
	{
		lr.positionCount = list.Count;
		int i = 0;
		foreach(Vector3 item in list)
		{
			lr.SetPosition(i, item);
			i++;
		}
	}
	public void ResetLine()
	{
		lr.positionCount = 0;
	}
}

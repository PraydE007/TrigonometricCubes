using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetController : MonoBehaviour
{
	public bool sinX = false;
	public bool sinY = false;
	public bool sinZ = false;
	public bool cosX = false;
	public bool cosY = false;
	public bool cosZ = false;
	public bool tanX = false;
	public bool tanY = false;
	public bool tanZ = false;
	public bool ctgX = false;
	public bool ctgY = false;
	public bool ctgZ = false;

	public static OffsetController singleton = null;

	private void Awake()
	{
		singleton = this;
	}

	public Vector3 CalculateOffset(float x, float y, float z)
	{
		Vector3 offset = Vector3.zero;

		offset += sinX ? new Vector3(0, Mathf.Sin(x), 0) : Vector3.zero;
		offset += sinY ? new Vector3(0, Mathf.Sin(y), 0) : Vector3.zero;
		offset += sinZ ? new Vector3(0, Mathf.Sin(z), 0) : Vector3.zero;
		offset += cosX ? new Vector3(0, Mathf.Cos(x), 0) : Vector3.zero;
		offset += cosY ? new Vector3(0, Mathf.Cos(y), 0) : Vector3.zero;
		offset += cosZ ? new Vector3(0, Mathf.Cos(z), 0) : Vector3.zero;
		offset += tanX ? new Vector3(0, Mathf.Tan(x), 0) : Vector3.zero;
		offset += tanY ? new Vector3(0, Mathf.Tan(y), 0) : Vector3.zero;
		offset += tanZ ? new Vector3(0, Mathf.Tan(z), 0) : Vector3.zero;
		offset += ctgX ? new Vector3(0, 1 / Mathf.Tan(x), 0) : Vector3.zero;
		offset += ctgY ? new Vector3(0, 1 / Mathf.Tan(y), 0) : Vector3.zero;
		offset += ctgZ ? new Vector3(0, 1 / Mathf.Tan(z), 0) : Vector3.zero;

		return offset;
	}

	private void OnValidate()
	{
		Debug.Log("Recalculating offset!");
		CubeController.RecalculateAll();
	}
}

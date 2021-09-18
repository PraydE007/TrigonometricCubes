using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	public Color startColor = Color.black;
	public float downStep = 5f;
	public float delay = 1f;
	public float stackDelay = 1f;

	private MeshRenderer renderer = null;

	private Color targetColor;
	private Vector3 startPoint;
	private Vector3 middlePoint;
	private Vector3 endPoint;

	private Vector3 realStartPoint;

	private Vector3 offset;

	private void Start()
	{
		startColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
		realStartPoint = transform.position;

		renderer = GetComponent<MeshRenderer>();
		renderer.material.color = startColor;

		delay += stackDelay;

		Recalculate();

		targetColor = new Color(
			renderer.material.color.r,
			renderer.material.color.g,
			renderer.material.color.b,
			1f
		);

		StartCoroutine(CustomUpdate());
	}

	public static void RecalculateAll()
	{
		CubeController[] cubes = GameObject.FindObjectsOfType<CubeController> ();

		foreach (CubeController cube in cubes)
		{
			cube.Recalculate();
		}
	}

	private void Recalculate()
	{
		if (OffsetController.singleton == null)
			return;

		offset = OffsetController.singleton.CalculateOffset(
			realStartPoint.x,
			realStartPoint.y,
			realStartPoint.z
		);

		// Debug.Log(name + " new offset: " + offset);

		startPoint = realStartPoint + offset;
		middlePoint = realStartPoint + (Vector3.down * downStep);
		endPoint = realStartPoint + (Vector3.down * (downStep * 2) + offset);
	}

	private IEnumerator CustomUpdate()
	{
		while (true)
		{
			transform.position = startPoint;
			renderer.material.color = startColor;

			yield return new WaitForSeconds(delay);

			while (Vector3.Distance(transform.position, middlePoint) > 0.1f)
			{
				transform.position = Vector3.Lerp(transform.position, middlePoint, 0.01f);
				renderer.material.color = Color.Lerp(renderer.material.color, targetColor, 0.01f);

				yield return null;
			}
			while (Vector3.Distance(transform.position, endPoint) > 0.1f)
			{
				transform.position = Vector3.Lerp(transform.position, endPoint, 0.01f);
				renderer.material.color = Color.Lerp(renderer.material.color, startColor, 0.01f);

				yield return null;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : MonoBehaviour 
{
	public float height = 1f;
	public float width = 2f;

	[SerializeField]
	protected GameObject m_BarPrefab;

	protected LineRenderer xAxis;
	protected LineRenderer yAxis;

	public LineRenderer NewLine()
	{
		return Object.Instantiate(m_BarPrefab).GetComponent<LineRenderer>();
	}

	public void DrawXAxis()
	{
		xAxis = NewLine();
		xAxis.useWorldSpace = false;
		xAxis.SetWidth(0.1f, 0.1f);
		Vector3[] xLine = new Vector3 [] { Vector3.zero, Vector3.right * width };
		xAxis.SetPositions(xLine);
	}

	public void DrawYAxis()
	{
		yAxis = NewLine();
		yAxis.useWorldSpace = false;
		yAxis.SetWidth(0.1f, 0.1f);
		Vector3[] yLine = new Vector3 [] { Vector3.zero, Vector3.up * height };
		yAxis.SetPositions(yLine);
	}
}

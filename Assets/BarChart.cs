using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChart : Chart
{
	public float pointSpacing 
	{
		get { return width / dataPoints.Count; }
	}

	public List<float> dataPoints;

	Dictionary<float, GameObject> bars = new Dictionary<float, GameObject>();

	public List<Color> colors;

	void Start () 
	{
		DrawXAxis();
		DrawYAxis();

		DrawChart();
	}

	public void DrawChart()
	{
		for (int i = 1; i <= dataPoints.Count; i++)
		{
			var obj = Object.Instantiate(m_BarPrefab);
			obj.transform.position += new Vector3(pointSpacing * i, 0f, 0f);

			var point = dataPoints[i - 1];
			var line = new Vector3 [] { Vector3.zero, new Vector3(0f, point, 0f) };
			var renderer = obj.GetComponent<LineRenderer>();
			renderer.startColor = colors[i-1];
			renderer.endColor = colors[i-1];

			renderer.startWidth = 0.1f;
			renderer.endWidth = 0.1f;

			renderer.SetPositions(line);

			bars.Add(point, obj);	
		}
	}

	float HeightForValue(float value)
	{
		return Mathf.Clamp(value, 0f, height); 
	}
	
	void Update () 
	{
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChart : Chart 
{

	public float pointSpacing 
	{
		get { return width / dataPoints.Count; }
	}

	public List<float[]> dataPoints = new List<float[]>();

	Dictionary<float, GameObject> bars = new Dictionary<float, GameObject>();

	public List<Color> colors;

	// Use this for initialization
	protected virtual void Start () 
	{
		dataPoints.Add(new float[] { 0f, 0.1f, 0.2f, 0.8f, 1f });
		dataPoints.Add(new float[] { 0f, 0.2f, 0.3f, 0.5f, 0.6f });
		dataPoints.Add(new float[] { 0f, 0.3f, 0.5f, 0.6f, 0.7f });
		dataPoints.Add(new float[] { 0f, 0.4f, 0.3f, 0.2f, 0.1f });
		dataPoints.Add(new float[] { 0f, 0.05f, 0.1f, 0.2f, 0.3f });

		
		for(int i = 0; i < 3; i++)
		{
			var line = dataPoints[i];
			var renderer = NewLine();
			renderer.startColor = colors[i];
			renderer.endColor = colors[i];
			
			Debug.Log(line.Length);
			var positions = new Vector3[line.Length];
			for(int n = 1; n <= positions.Length; n++)
			{
				positions[n - 1] = new Vector3(pointSpacing * n, line[n - 1], 0f);
			}

			renderer.SetPositions(positions);
		}
	}
	
	void Update () 
	{
		
	}
}

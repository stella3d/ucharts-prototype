using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GCLineChart : LineChart 
{
	public int updateSkipDivider = 3;

	public float yAxisCenter = 5f;
	public float xAxisSpacing = 0.1f;

	protected const int k_SampleCount = 100;	

	protected float[] m_TotalAllocSamples = new float[k_SampleCount];	

	protected LineRenderer m_LineRenderer;

	int sampleIndex;
	int updateCount;

	protected override void Start () 
	{
		DrawXAxis();
		DrawYAxis();

		m_LineRenderer = NewLine();
		m_LineRenderer.startColor = colors[0];
		m_LineRenderer.endColor = colors[0];

		var positions = new Vector3[k_SampleCount];

		// set a flat line on the bottom of the Y axis to start
		for(int n = 1; n <= positions.Length; n++)
		{
			positions[n - 1] = new Vector3(xAxisSpacing * (float)n, 0f, 0f);
		}

		m_LineRenderer.SetPositions(positions);
	}

	Vector3[] m_Positions = new Vector3[k_SampleCount];
	Vector3[] m_PositionShiftCache = new Vector3[k_SampleCount];
	
	void Update () 
	{
		if(updateCount % updateSkipDivider == 0)
		{
			//Array.Clear(m_PositionShiftCache, 0, m_PositionShiftCache.Length);
			m_LineRenderer.GetPositions(m_Positions);

			for(int i = 0; i < m_Positions.Length - 1; i++)
			{
				var pointY = m_Positions[i + 1].y;
				var previous = m_Positions[i];
				previous.y = pointY;
				m_PositionShiftCache[i] = previous;
			}

			var bytes = GC.GetTotalMemory(false);
			m_TotalAllocSamples[sampleIndex] = bytes;
			m_PositionShiftCache[99] = BytesToChartPosition(bytes, m_Positions[99]);

			m_LineRenderer.SetPositions(m_PositionShiftCache);
			
			if(sampleIndex < k_SampleCount - 1)
				sampleIndex++;
			else
				sampleIndex = 0;				
		}

		updateCount++;
	}

	// for now we assume 1 billion bytes = top of the graph = 10f height
	Vector3 BytesToChartPosition(long byteCount, Vector3 previous)
	{
		//Debug.Log("bytes: " + byteCount);
		float y = byteCount / 1000f / 1000f;
		return new Vector3(previous.x, y, previous.z);
	}
}

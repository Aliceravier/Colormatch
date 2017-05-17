using UnityEngine;
using System.Collections;


public class Grid : MonoBehaviour {

	public float width = 0.5f;
	public float height = 0.5f;

	public float screenHeight = 800.0f;
	public float screenWidth = 1200.0f;

	void DrawSomeLines(float spacing, float limit, float cam, bool vert)
	{
		float bigNum = 100000000000.0f;
		Vector3 startPoint;
		Vector3 endPoint;

		for (float pos = cam - screenWidth; pos < cam + limit; pos += spacing) 
		{
			if (vert) {
				startPoint = new Vector3 (Mathf.Floor (pos / spacing) * spacing, -bigNum, 0.0f);
				endPoint = new Vector3 (Mathf.Floor (pos / spacing) * spacing, bigNum, 0.0f);
			} 
			else {
				startPoint = new Vector3 (-bigNum, Mathf.Floor (pos / spacing) * spacing, 0.0f);
				endPoint = new Vector3 (bigNum, Mathf.Floor (pos / spacing) * spacing, 0.0f);
			}

			Gizmos.DrawLine (startPoint, endPoint);

		}
	}
	void OnDrawGizmos() 
	{
		
		Vector3 pos = Camera.current.transform.position;

		DrawSomeLines (height, screenHeight, pos.y, true);
		DrawSomeLines (width, screenWidth, pos.x, false);

	}


}

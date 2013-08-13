
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Vectrosity;

public class DrawLine_to_walls:MonoBehaviour{

public Material lineMaterial;
public int maxPoints = 300;
public float lineWidth = 2.0f;
public int minPixelMove = 5;

private Vector2[] linePoints;
private VectorLine line;
private int lineIndex = 0;
private Vector2 previousPosition;
private int sqrMinPixelMove;
private bool canDraw = false;


void Start () {
	if (maxPoints%2 != 0) maxPoints++;  // No odd numbers
	linePoints = new Vector2[maxPoints];
	line = new VectorLine("DrawnLine", linePoints, lineMaterial, lineWidth);
	sqrMinPixelMove = minPixelMove*minPixelMove;
}

void Update () {
	Vector3 mousePos = Input.mousePosition;
	if (Input.GetMouseButtonDown(0)) {
		line.ZeroPoints ();
		previousPosition = mousePos;
		lineIndex = 0;
		line.Draw ();
		canDraw = true;
	}
	else if (Input.GetMouseButton(0) && ((Vector2)mousePos - (Vector2)previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw) {
		linePoints[lineIndex++] = previousPosition;
		linePoints[lineIndex++] = mousePos;
		previousPosition = mousePos;
			if (lineIndex >= maxPoints) {

								canDraw = false;
			}
						line.Draw ();
	}
}

}
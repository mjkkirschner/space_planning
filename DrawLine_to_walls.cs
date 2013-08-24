
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Vectrosity;
using System;

public class DrawLine_to_walls:MonoBehaviour{

public Material lineMaterial;
public int maxPoints = 300;
public float lineWidth = 2.0f;
public int minPixelMove = 1;

private Vector3[] linePoints;
private VectorLine line;
private int lineIndex = 0;
private Vector3 previousPosition;
private int sqrMinPixelMove;
private bool canDraw = false;
public Mesh originalMesh;
public Vector3 mousePos;

void Start () {
	if (maxPoints%2 != 0) maxPoints++;  // No odd numbers
	linePoints = new Vector3[maxPoints];
	
	sqrMinPixelMove = minPixelMove*minPixelMove;
				Debug.Log ("START");
}



void OnPress(bool state){

		if (state == true) {

		line = new VectorLine("DrawnLine", linePoints, lineMaterial, lineWidth);
		line.ZeroPoints ();

		previousPosition = mousePos;
		lineIndex = 0;
		line.Draw3D ();
		canDraw = true;
		Debug.Log ("OH NO");
				
		}



		else if (state == false){


			Vector3[] orignalverts = line.vectorObject.GetComponent<MeshFilter> ().mesh.vertices;



			List<Vector3> foos = new List<Vector3>(orignalverts);	


			for (int i = foos.Count; i --> 0; )
			{
				Vector3 point = foos [i];

				if ((point - Vector3.zero).sqrMagnitude < .01){


					foos.Remove (point);
				}
			}



			Debug.Log (foos.Count);
			orignalverts = foos.ToArray ();

			int[] triangles = line.vectorObject.GetComponent<MeshFilter> ().mesh.triangles;
			Debug.Log ("Raw tri len");
			Debug.Log (triangles.Length);

			Array.Resize<int> (ref triangles, (int)((orignalverts.Length)*1.5));

			Debug.Log ("resized tri len");
			Debug.Log (triangles.Length);	


			Vector3[] newverts = new Vector3 [orignalverts.Length];	
			System.Array.Copy(orignalverts, newverts, newverts.Length);


			int[] newtriangles = new int[triangles.Length];	
			System.Array.Copy(triangles, newtriangles, newtriangles.Length);

			Debug.Log ("new tri len");
			Debug.Log (newtriangles.Length);	



			for( int i = 0; i < newtriangles.Length; i++) 

			{

				newtriangles[i] += orignalverts.Length;
				Debug.Log(newtriangles[i]);
			}


			for( int i = 0; i < newverts.Length; i++) 

			{

				newverts[i] += new Vector3 (0,-10,0);



			}


			List<int> sidetris = new List<int> ();


			//			for (int i = 0; i < 50; i++)
			//			{
			//				sidetris[i] = newverts[i];
			//				sidetris[i+1] = orignalverts[i];
			//				sidetris[i+2] = newverts[i+1];
			//
			//
			//				sidetris[i + 3] = newverts [i + 1];
			//				sidetris[i + 4] = orignalverts[i];
			//								sidetris [i + 5] = orignalverts [i + 1];
			//
			//
			//
			//			
			//			}

			//				for (int i = 0; i < 197; i++)
			//				{
			//					sidetris.Add(200+i);
			//					sidetris.Add(i);
			//					sidetris.Add(201+i+1);
			//
			//
			//					sidetris.Add(201 + i+1);
			//					sidetris.Add(i);
			//					sidetris.Add(i+2);
			//
			//
			//					sidetris.Add(201+i+2);
			//					sidetris.Add(i+1);
			//					sidetris.Add(200+i+1);
			//
			//
			//
			//					sidetris.Add(i+3);
			//					sidetris.Add(i+1);
			//					sidetris.Add(201 + i+2);
			//
			//
			//
			//
			//
			//
			//
			//
			//
			//				}

			Debug.Log (orignalverts.Length);
			for (int i = 0; i < ((orignalverts.Length))-3; i++)
			{
				Debug.Log(orignalverts.Length + i);
				sidetris.Add((orignalverts.Length)+i);
				sidetris.Add(i);
				sidetris.Add((orignalverts.Length)+1+i+1);


				sidetris.Add((orignalverts.Length)+1 + i+1);
				sidetris.Add(i);
				sidetris.Add(i+2);


				sidetris.Add((orignalverts.Length)+1+i+2);
				sidetris.Add(i+1);
				sidetris.Add((orignalverts.Length)+i+1);



				sidetris.Add(i+3);
				sidetris.Add(i+1);
				sidetris.Add((orignalverts.Length)+1 + i+2);









			}




			List<Vector3> combinedvert = new List<Vector3> (); 
			List<int> combinedtri = new List<int> ();

			combinedvert.AddRange (orignalverts);
			combinedvert.AddRange (newverts);




			combinedtri.AddRange (triangles);
			combinedtri.AddRange (newtriangles);
			combinedtri.AddRange (sidetris);
			Mesh alteredmesh  = new Mesh ();



			line.vectorObject.GetComponent<MeshFilter> ().mesh = alteredmesh;

			alteredmesh.vertices = combinedvert.ToArray();
			alteredmesh.triangles = combinedtri.ToArray();

			Debug.Log(orignalverts[0]);
			Debug.Log (newverts [0]);

			Debug.Log (combinedvert.Count);
			Debug.Log (combinedtri.Count);

			//						foreach (Vector3 vert in combinedvert) {
			//
			//								Instantiate (tinysphere, vert, Quaternion.identity);
			//
			//						}

			//line.Draw3D ();

		}


		}









void Update () {
	
	mousePos = Input.mousePosition;
	
	}

	void OnDrag(Vector2 delta){

	 if ((mousePos - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw) {
						Debug.Log ("OMG");
		linePoints[lineIndex++] = new Vector3(previousPosition.x,0,previousPosition.y);
		linePoints[lineIndex++] = new Vector3 (mousePos.x,0,mousePos.y);
		
				
		previousPosition = mousePos;
			if (lineIndex >= maxPoints) {

								canDraw = false;
			}
						line.Draw3D ();
		}

	}

		



				



				



	}

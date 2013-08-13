using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Vectrosity;

public class SpaceNode : MonoBehaviour {

	public GameObject tinysphere;
	public float distance;
	public GameObject window;
	public List<GameObject> nodes_notself;
	private float offset = 1; 
	private float size;
	public VectorLine line;
	public Camera vectorCam;
	public float sizeval;
	public GameObject follower;
	public GameObject center_follower;
	public bool flag = false;


	// Use this for initialization
	void Start () {
				

		center_follower = new GameObject ();
		center_follower.transform.localPosition = Vector3.zero;
				center_follower.transform.Rotate (new Vector3 (90, 0, 0));


		follower = new GameObject ();
		follower.layer = 2;
		follower.transform.parent = this.transform;
		follower.transform.localPosition = Vector3.zero;
		follower.AddComponent<SphereCollider> ();
		((SphereCollider)follower.collider).radius = 1;
				
				
				// create the line for each circle
				line = new VectorLine("circle", new Vector3[100], null, 1f);
				
				// grab all the currently created objects except for self.

				GameObject[] nodes = GameObject.FindGameObjectsWithTag ("Player");
				nodes_notself = (from node in nodes
								where node.transform.position != this.gameObject.transform.position
			       				select node).ToList();


				
			

						
	}	


	public void init(){
		// attach the button script activate to the UI window, as its target

	 		var button = this.gameObject.GetComponent<UICheckbox>();
			button.eventReceiver = this.gameObject.GetComponent<AddUnitHUD>().prefabinstance;
			window = button.eventReceiver;

		UISlider[] sliders = window.GetComponentsInChildren<UISlider> ();
		foreach (UISlider slider in sliders)
			{

						slider.eventReceiver = this.gameObject;

			}

	}

	// Update is called once per frame
	void Update () {
				
				
				
				
				

			this.gameObject.transform.localScale = new Vector3 (offset, offset, offset);
				
			Vector3 centroidpos = new Vector3(0,0,0);
			//create a centroid variable


			foreach (GameObject node in nodes_notself)
			{
				
				
				centroidpos += node.transform.localPosition;
				//generate the centroid from all spheres.
						
			}

			centroidpos = (centroidpos/(float)(nodes_notself.Count));
			//final centroid calculation
		centroidpos = centroidpos-transform.localPosition;
				this.gameObject.rigidbody.AddForce (centroidpos*2);
				Debug.DrawRay (this.gameObject.transform.localPosition, centroidpos);
			// move towards center

				center_follower.transform.localPosition = transform.localPosition;

		// make a circle from the line objects points of size, offset at 0,0,0)
		line.MakeCircle(Vector3.zero,size);


		follower.GetComponent<SphereCollider> ().radius = size/this.transform.localScale.x;
		// generate a circle at 0,0,0 of variable size.
		line.Draw3D (center_follower.transform);
		//draw the line ( the circle).







		//line.vectorObject.transform.localPosition = this.gameObject.transform.localPosition;
		// move the line object to the position of the sphere



		//rotate the line by 90 degrees

		//line.vectorObject.transform.localPosition = new Vector3 (line.vectorObject.transform.localPosition.x-.5f, line.vectorObject.transform.localPosition.y + 246, line.vectorObject.transform.localPosition.z-.5f);

		// move the line up.

		line.vectorObject.layer = 0;

		// change the layer of the line so that it is drawn by the main camera.


				
				
		if (flag == false){

					

				Vector3[] orignalverts = line.vectorObject.GetComponent<MeshFilter> ().mesh.vertices;
			
				int[] triangles = line.vectorObject.GetComponent<MeshFilter> ().mesh.triangles;
			

						Vector3[] newverts = new Vector3 [orignalverts.Length];	
						System.Array.Copy(orignalverts, newverts, newverts.Length);

				
			int[] newtriangles = new int[triangles.Length];	
			System.Array.Copy(triangles, newtriangles, newtriangles.Length);



				
		for( int i = 0; i < newtriangles.Length; i++) 

				{
						
				newtriangles[i] += orignalverts.Length;
				}


		for( int i = 0; i < newverts.Length; i++) 
				
				{
								
								newverts[i] += new Vector3 (0,.5f,0);
								
				

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

			for (int i = 0; i < 197; i++)
			{
				sidetris.Add(200+i);
				sidetris.Add(i);
				sidetris.Add(201+i+1);


				sidetris.Add(201 + i+1);
				sidetris.Add(i);
				sidetris.Add(i+2);


				sidetris.Add(201+i+2);
				sidetris.Add(i+1);
				sidetris.Add(200+i+1);



				sidetris.Add(i+3);
				sidetris.Add(i+1);
				sidetris.Add(201 + i+2);









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

//						foreach (Vector3 vert in combinedvert) {
//
//								Instantiate (tinysphere, vert, Quaternion.identity);
//
//						}
				
				flag = true;
		}

	}




		public void OnSliderOffsetChange(float Value)
	
	
		{

		this.offset = Value+1;

		//offset = the slider value + 1

		this.offset = offset * 2;

		//multiply offset by 2

		OnSliderSizeChange (sizeval);

		// update the size of the circle based on the newly changed first slider

		}
	



	public void OnSliderSizeChange(float Value)


	{

	
	this.size = Value + this.offset;
				sizeval = Value;
				

	}

}

using UnityEngine;
using System.Collections;

public class UIsetup : MonoBehaviour {

	public GameObject listener;

	// Use this for initialization
	void Start () {


				UICamera.fallThrough = listener;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

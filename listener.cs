using UnityEngine;
using System.Collections;

public class listener : MonoBehaviour {
	public GameObject forwardevents;
	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {

				

	}


	void OnPress(bool state){
	forwardevents.SendMessage("OnPress",state);


	
	
	}

	void OnDrag(Vector2 delta){
		forwardevents.SendMessage("OnDrag",delta);




	}


}

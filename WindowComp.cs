using UnityEngine;
using System.Collections;

public class WindowComp : MonoBehaviour {

	private UIPanel panel; 


void Awake ()
		{

				 panel = this.GetComponent<UIPanel> ();  

		}

 void OnActivate (bool state)
		{

				Debug.Log ("called check");
	
				if ( state == true) {
						panel.alpha = 1;
				} else {
						panel.alpha = 0;
				}




	
		}
}
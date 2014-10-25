using UnityEngine;
using System.Collections;

public class ExitButton2 : MonoBehaviour {
	public bool clicked = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transitions.currBattle != "wakeup"){
			clicked = false;
		}
	}
	void OnMouseDown(){
		clicked = true;
	}
	void OnMouseEnter(){
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("exitbutton_hl");
	}
	void OnMouseExit(){
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("exitbutton");
	}
}

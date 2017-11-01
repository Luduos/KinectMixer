using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingTheFluid : MonoBehaviour {
	public GameObject FillingComp ;
	//private Canvas FillingCompCanvas;
	private Image FillingCompImageColour;


	void OnParticleCollision(GameObject other){

	//FillingCompCanvas = FillingComp.GetComponent<Canvas>();
	FillingCompImageColour = FillingComp.GetComponent<Image>();

	//FillingCompImageColour = sum Of the SelectedObjects colours;
	FillingCompImageColour.fillAmount +=0.05f;
	Debug.Log("filling");



	Debug.Log(other +"is the object in parameters");
	Debug.Log("Collision detected from PS Script");


	}
}

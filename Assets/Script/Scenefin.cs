using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenefin : MonoBehaviour {

	public GameObject panneau1, panneau2, panneau3, panneau4;
	int compteur = 0;

	public Image fadeImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (compteur == 4) {
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + 0.01f);
		}

		if (fadeImage.color.a == 1 && compteur == 4) {
			Application.Quit();
		}
	}


	public void pressedbutton1 () {
		compteur += 1;

		Destroy (panneau1);
	}
	public void pressedbutton2 () {
		compteur += 1;

		Destroy (panneau2);
	}
	public void pressedbutton3 () {
		compteur += 1;

		Destroy (panneau3);
	}
	public void pressedbutton4 () {
		compteur += 1;

		Destroy (panneau4);
	}

}

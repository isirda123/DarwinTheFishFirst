using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadenoirscenefin : MonoBehaviour {

	public Image fadeImage;

	bool put = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeImage.color.a > 0 && put == false) {
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - 0.01f);
		}

		if (fadeImage.color.a < 0) {
			put = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControle : MonoBehaviour {

	public GameObject bite;

	float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 10) {
			bite.SetActive (false);
		}
	
	
	}
}

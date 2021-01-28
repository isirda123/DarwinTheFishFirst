using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deplacement : MonoBehaviour {



	FMOD.Studio.EventInstance musiqueAmbiance;
	FMOD.Studio.EventInstance Questions;
	FMOD.Studio.ParameterInstance Tempo;
	FMOD.Studio.EventInstance voix1;
	FMOD.Studio.EventInstance voix2;
	FMOD.Studio.EventInstance voix3;
	FMOD.Studio.EventInstance voix4;
	FMOD.Studio.EventInstance voix5;


	//Fadeaunoir
	bool contactvoiture = false;





	public GameObject Floor;
	public GameObject Runner;
	public GameObject monopedeCanvas;
	public GameObject bipedeCanvas;
	public GameObject TripedeCanvas;
	public GameObject QuadripedeCanvas;
	public GameObject ChangementAvatar;
	public GameObject Avataranimation;
	public GameObject cameraAvatar;
	public GameObject Blocus;

	public Image fadeImage;
	public Image fondcolorer;




	Animator animator;


	public Text Longeur;

	Rigidbody rb;

	public int Character = 1;
	int distance = 0;
	int nbinput = 0;


	float Movement = 0;
	float velocityX;

	bool StopInstantiate = false;
	bool oneframe = false;
	bool changementcamera = false;

	float timer = 0;
	float pute = 1f;

	bool enculer = false;


	bool un = false;
	bool deux = false;
	bool trois = false;
	bool quatre = false;
	bool cinq	= false;


	// Use this for initialization
	void Start () {
		
		Questions = FMODUnity.RuntimeManager.CreateInstance ("event:/Questions");
		musiqueAmbiance.getParameter ("Vitesse/Tempo", out Tempo);
		voix1 = FMODUnity.RuntimeManager.CreateInstance ("event:/Narateur 1");
		voix2 = FMODUnity.RuntimeManager.CreateInstance ("event:/Narateur 2");
		voix3 = FMODUnity.RuntimeManager.CreateInstance ("event:/Narateur 3");
		voix4 = FMODUnity.RuntimeManager.CreateInstance ("event:/Narateur 4");
		voix5 = FMODUnity.RuntimeManager.CreateInstance ("event:/Narateur 5");
		voix1.start ();



		rb = Runner.GetComponent<Rigidbody>();
		animator = Avataranimation.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		if (distance < 100 && enculer == false) {
			musiqueAmbiance = FMODUnity.RuntimeManager.CreateInstance ("event:/Musique Poisson");
			musiqueAmbiance.start ();
			enculer = true;
		}
		if (contactvoiture == true) {
			
			musiqueAmbiance.setVolume (pute - 0.1f);
			pute -= 0.1f * Time.deltaTime;
		}

		if (timer > 5) {
			Questions.start ();
			timer = 0;
		}

		Avataranimation.transform.position = new Vector3 (Avataranimation.transform.position.x, -3.3f,Avataranimation.transform.position.z);

		if (contactvoiture == true) {
			Avataranimation.transform.localScale = new Vector3 (Avataranimation.transform.localScale.x - 0.01f, Avataranimation.transform.localScale.y - 0.01f, Avataranimation.transform.localScale.z - 0.01f);
//			fadeImage.DOFade(1, fadeDurationSeconds).SetDelay(delayBeforeFadingIn).SetEase(fadeEase);
			fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + 0.01f);
			if (fadeImage.color.a >= 1) {
				Questions.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
				musiqueAmbiance.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
				SceneManager.LoadScene (1);
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}

		oneframe = false;


			

		if (Movement != 0 && Movement > 0)
        {
			run ();
		}

		velocityX = rb.velocity.x;

		animator.speed = velocityX * 4f * Time.deltaTime;

		distance = (int)Runner.transform.position.x;

		animator.SetFloat("distance", distance);


		if (distance <300) {
			monopedeCanvas.SetActive (true);
			CulDeJatte ();
		}


		if (distance > 300 && distance < 600) {
			Tempo.setValue (0.5f);
			monopedeCanvas.SetActive (false);
			bipedeCanvas.SetActive (true);
			biped ();
		}

		if (distance > 300 && un == false) {
			voix2.start ();
			un = true;
		}

		if (distance > 600 && deux == false) {
			voix3.start ();
			deux = true;
		}



		if (distance > 600 && distance < 1200) {
			Tempo.setValue (1f);
			bipedeCanvas.SetActive (false);
			QuadripedeCanvas.SetActive (true);
			bipedbras ();
		}

		if (distance > 1200 && StopInstantiate == false) {
			Instantiate (Blocus, new Vector3 (Runner.transform.position.x +25, Runner.transform.position.y, Runner.transform.position.z), new Quaternion (0,0,0,0));
			StopInstantiate = true;
		}

		//		if (Character == 3) {
//			TripedeCanvas.SetActive (true);
//			triped ();
//		}
//		if (Character == 4) {
//			QuadripedeCanvas.SetActive (true);
//			quadripede ();
//		}
	}


	public void OnTriggerEnter (Collider col) {
		if (col.tag == "Floor") {
			Instantiate (Floor, new Vector3 (col.transform.position.x + 25f, col.transform.position.y, 0), new Quaternion (0,0,0,0));
		}
	}

	public void OnTriggerExit (Collider col) {
		if (col.tag == "Floor") {
			Destroy (col.gameObject);
		}
	}

	public void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "ChangementAvatar") {
			Character += 1;
			StopInstantiate = false;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "changementniveau") {
			voix5.start ();
			contactvoiture = true;
			QuadripedeCanvas.SetActive (false);
		}
	}


	void CulDeJatte () {
		if (Input.GetKeyDown (KeyCode.Q))
		{
			Movement += 25;
		}
	}

	void biped ()
	{
		if (Input.GetKeyDown (KeyCode.Q) && nbinput == 0 && oneframe == false) {
			
			nbinput = 1;
			oneframe = true;
		}
		if (Input.GetKeyDown (KeyCode.Q) && nbinput != 0 && oneframe == false) {
			Movement -= Movement * 0.5f * Time.deltaTime;
			oneframe = true;
		}





		if (Input.GetKeyDown (KeyCode.D) && nbinput == 1 && oneframe == false) {
			Movement += 45;
			nbinput =0;
			oneframe = true;

		}
		if (Input.GetKeyDown (KeyCode.D) && nbinput != 1 && oneframe == false) {
			Movement -= Movement * 0.5f * Time.deltaTime;
			oneframe = true;

		}

	}

	void bipedbras () {
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.K) && nbinput == 0 && oneframe == false) {
			nbinput =1;
			oneframe = true;
		}
		if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.M) && nbinput == 1 && oneframe == false) {
			nbinput =0;
			oneframe = true;
			Movement += 60;
		}
	}

















//
//	void triped () {
//		
//		if (Input.GetKeyDown (KeyCode.Z) && nbinput == 0 && oneframe == false) {
//
//			nbinput = 1;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.Z) && nbinput != 0 && oneframe == false) {
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput == 1 && oneframe == false) {
//
//			nbinput = 2;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput != 1 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.D) && nbinput == 2 && oneframe == false) {
//
//			nbinput = 0;
//			oneframe = true;
//			Movement += 50;
//		}
//		if (Input.GetKeyDown (KeyCode.D) && nbinput != 2 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//	}
//
//
//	void quadripede () {
//
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput == 0 && oneframe == false) {
//
//			nbinput = 1;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.Q) && nbinput != 0 && oneframe == false) {
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//		if (Input.GetKeyDown (KeyCode.S) && nbinput == 1 && oneframe == false) {
//
//			nbinput = 2;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.S) && nbinput != 1 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.D) && nbinput == 2 && oneframe == false) {
//
//			nbinput = 3;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.D) && nbinput != 2 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//
//
//
//		if (Input.GetKeyDown (KeyCode.F) && nbinput == 3 && oneframe == false) {
//			Movement += 80;
//			nbinput = 1;
//			oneframe = true;
//		}
//		if (Input.GetKeyDown (KeyCode.F) && nbinput != 3 && oneframe == false) {
//			nbinput = 0;
//			Movement -= Movement * 0.5f;
//			oneframe = true;
//		}
//	}
//




	void run ()
	{
		rb.AddForce (new Vector3 (Movement * 0.04f, 0, 0));
		//print(Movement);
        //print(rb.velocity);
        if (rb.velocity.x > 35)
        {
            rb.velocity = new Vector3 (35, rb.velocity.y, rb.velocity.z);
        }
		Movement -= (Movement * 0.4f) * Time.deltaTime;
		Movement = Mathf.Max(Movement, 0);
	}
}

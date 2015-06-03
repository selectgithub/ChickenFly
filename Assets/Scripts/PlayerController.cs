using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	
	public Rigidbody2D egg;

	public AudioClip shootAudio;
	
	private Animator animator;
	private float maxSpeed = 3.5f;
	private float minFlySpeed = 2.2f;
	private float minSpeed = 1.0f;
	private float highestHeight = 4.3f;
	private float lowestHeight = -2.65f;
	
	private float incrementSpeed = 0.26f;// if click to give the power,increse the speed
	private float decrementSpeed = 0.015f;//every frame decrese the speed
	
	private float raiseUpSpeed = 0.25f;//once start fly, the player will higher and higher.
	private float fallDownSpeed = 3.0f;//once no power to fly, the player will falldown.
	
	private float fireRate = 0.5f;
	private float nextFire = 0.0f;
	
	public static float chickenSpeed;

	public delegate void PlayerDelegate();
	public event PlayerDelegate OnTouchEvent;
	public event PlayerDelegate OnTapFoxEvent;

	public event PlayerDelegate RunEvent;
	public event PlayerDelegate FlyEvent;

	private AudioSource source;

	int i = 0;
	
	void Awake(){
		animator = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}
	
	// Use this for initialization
	void Start () {
		fireRate = 0.5f - (GameController.Level / 61.8f);
		incrementSpeed = 0.26f + (GameController.Level / 61.8f);
		raiseUpSpeed = 0.25f + (GameController.Level / 61.8f);
	}
	
	void OnPointedOutFox(Transform foxTransform){
		if(Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Rigidbody2D clone;
			clone = Instantiate(egg, transform.position, transform.rotation) as Rigidbody2D;
			clone.velocity = transform.TransformDirection(new Vector3(foxTransform.position.x - transform.position.x,foxTransform.position.y - transform.position.y,0.0f) * 3);

			//source.clip = shootAudio;
			//source.Play();
		}
		if(OnTapFoxEvent != null){
			OnTapFoxEvent();
		}
	}
	
	void OnDestroy(){
		//Debug.Log ("Player die!");
	}
	
	
	void FixedUpdate(){
		chickenSpeed = animator.GetFloat ("ChickenSpeed");
		if(Input.GetKeyUp(KeyCode.F)){
			//once click to give the player power, raise the speed up.
			if(chickenSpeed <= maxSpeed){
				chickenSpeed += incrementSpeed;
				animator.SetFloat("ChickenSpeed",chickenSpeed);
			}
			if(OnTouchEvent != null){
				OnTouchEvent();
				Debug.Log("XXXX");
			}
		}
		
		i = 0;
		while (i < Input.touchCount) {
			if (Input.GetTouch(i).phase == TouchPhase.Began){
				//once click to give the player power, raise the speed up.
				if(Input.GetTouch(i).position.x < (Screen.width / 2)){
					if(chickenSpeed <= maxSpeed){
						chickenSpeed += incrementSpeed;
						animator.SetFloat("ChickenSpeed",chickenSpeed);
					}
				}
			}
			++i;
			if(OnTouchEvent != null){
				OnTouchEvent();
			}
		}
		
		if(chickenSpeed >= minSpeed){
			//always drag down the speed
			chickenSpeed -= decrementSpeed;
			animator.SetFloat("ChickenSpeed",chickenSpeed);
		}
		if (chickenSpeed > minFlySpeed) {
			if(FlyEvent != null){
				FlyEvent();
			}
			if (transform.position.y < highestHeight) {
				//start raise up
				transform.Translate (Vector2.up * raiseUpSpeed * Time.deltaTime);
			}
		} else {
			if(RunEvent != null){
				RunEvent();
			}
			if (transform.position.y > lowestHeight) {
				//start fall down
				transform.Translate (-Vector2.up * fallDownSpeed * Time.deltaTime);
			}
		}
		
	}
}

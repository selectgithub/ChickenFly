using UnityEngine;
using System.Collections;

public class DreamlanSpriteController : MonoBehaviour {

	public SpriteRenderer[] backgrounds;
	public SpriteRenderer[] middleLayers;
	public SpriteRenderer[] floor;

	public Sprite[] backgroundSprites;
	public Sprite[] middleLayerSprites;
	public Sprite[] floorSprites;

	void Awake(){
		int a = Random.Range (0,backgroundSprites.Length);
		foreach(SpriteRenderer sr in backgrounds){
			sr.sprite = backgroundSprites[a];
		}

		a = Random.Range (0,middleLayerSprites.Length);
		foreach(SpriteRenderer sr in middleLayers){
			sr.sprite = middleLayerSprites[a];
		}

		a = Random.Range (0,floorSprites.Length);
		foreach(SpriteRenderer sr in floor){
			sr.sprite = floorSprites[a];
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

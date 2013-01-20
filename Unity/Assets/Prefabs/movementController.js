#pragma strict
var speed : float;
var rotation : float;
var plane : GameObject;
var textures : Texture[];
var textureNumber : int;
var walkLoop : int;
var walkLoopDelay : int;
var idleLoop : int;
var idleLoopDelay : int;
var horMovement : float;
var idleDir : int;
var footsteps : AudioClip[];

function Start () {
	speed = 100.0;
	rotation = 0;
	walkLoop = 0;
	walkLoopDelay = 0;
	idleLoop = 0;
	idleLoopDelay = 0;
	idleDir = 1;
	plane = GameObject.Find("player");
	textureNumber = 0;
	
	plane.transform.position = transform.position;	
}

function playRandomSounds () {
	audio.clip = footsteps[Random.Range(0,footsteps.length)];
    audio.Play();
}

function Update() {
	horMovement = Input.GetAxis("Horizontal");

	if (horMovement == 0) {
		idleLoopDelay = idleLoopDelay + 1;
		if (idleLoopDelay % 75 == 0) {
			idleLoop = idleLoop + (idleDir);
		}
		textureNumber = idleLoop;
		if (idleLoop == 2) {
			idleDir = -1;
		}
		if (idleLoop == 0) {
			idleDir = 1;
		}
	} else {
		idleLoopDelay = 0;
		idleLoop = 0;
		idleDir = 1;
	}
 	
	if (horMovement) {
		if (horMovement < 0) {
			rotation = 0;
		}
		if (horMovement > 0) {
			rotation = 180;
		}
		if (Mathf.Abs(horMovement) > 0.1) {
			textureNumber = 1;
		}
		if (Mathf.Abs(horMovement) > 0.5) {
			textureNumber = 2;
		}
		if (Mathf.Abs(horMovement) == 1) {
			//Camera.main.orthographicSize = 256;    
			walkLoopDelay = walkLoopDelay + 1;
			if (walkLoop == 6 || walkLoop == 3) {
				playRandomSounds();
			}
			if (walkLoopDelay % 25 == 0) {
				walkLoop = walkLoop + 1;
			}
			textureNumber = walkLoop + 2;
			if (walkLoop > 7) {
				walkLoop = 2;
				walkLoopDelay = 0;
			}
		} else {
			//Camera.main.orthographicSize = 768;
			walkLoopDelay = 0;
			walkLoop = 0;
			textureNumber = 2;
		}
		if (textureNumber > 3) {
			transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
			plane.transform.position = transform.position;
		}
	}
	plane.renderer.sharedMaterial.mainTexture = textures[textureNumber];
	plane.transform.rotation = Quaternion.Euler(0,rotation,0);
}
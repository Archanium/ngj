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

function Start () {
	speed = 50.0;
	rotation = 0;
	walkLoop = 0;
	walkLoopDelay = 0;
	idleLoop = 0;
	idleLoopDelay = 0;
	plane = GameObject.Find("player");
	textureNumber = 0;
	
	plane.transform.position = transform.position;	
}

function Update() {
 	var horMovement = Input.GetAxis("Horizontal");

	
	if (horMovement == 0) {
		idleLoopDelay = idleLoopDelay + 1;
		if (idleLoopDelay % 75 == 0) {
			idleLoop = idleLoop + 1;
		}
		textureNumber = idleLoop;
		if (idleLoop > 2) {
			idleLoop = 0;
			idleLoopDelay = 0;
		}
	} else {
		idleLoopDelay = 0;
		idleLoop = 0;
	}
 	
	if (horMovement) {
		if (horMovement < 0) {
			rotation = 0;
			textureNumber = 1;
		}
		if (horMovement > 0) {
			rotation = 180;
			textureNumber = 1;
		}
		if (horMovement == 1 || horMovement == -1) {
			if (walkLoopDelay == 0) {
				this.audio.Play();
			}
			walkLoopDelay = walkLoopDelay + 1;
			if (walkLoopDelay % 25 == 0) {
				walkLoop = walkLoop + 1;
			}
			textureNumber = walkLoop + 2;
			if (walkLoop > 7) {
				walkLoop = 2;
				walkLoopDelay = 0;
			}
		} else {
			walkLoopDelay = 0;
			walkLoop = 0;
		}
		transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
		plane.transform.position = transform.position;
	}
	plane.renderer.sharedMaterial.mainTexture = textures[textureNumber];
	plane.transform.rotation = Quaternion.Euler(0,rotation,0);
}
#pragma strict

function Start () {

}

var speed : float = 100.0;
var rotation : float = 0;

function Update() {
 	var horMovement = Input.GetAxis("Horizontal");

	if (horMovement) {
		if (horMovement < 0) {
			rotation = 180;
		} else if (horMovement > 0) {
			rotation = 0;
		}		
		transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
	}
	transform.rotation = Quaternion.Euler((Time.frameCount * Time.deltaTime) % 10,rotation,0);
}
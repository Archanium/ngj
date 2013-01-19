#pragma strict

function Start () {

}

var speed : float = 1000.0;
var textureLeft : Texture2D;
var textureRight : Texture2D;
 
function Update() {
 	var horMovement = Input.GetAxis("Horizontal");

	transform.Rotate(Mathf.Sin(Time.frameCount) * 15,0,0);
 	 	
	if (horMovement) {
		if (horMovement > 0) {
			this.renderer.sharedMaterial.mainTexture = textureRight;
		}
		
		if (horMovement < 0) {			
			this.renderer.sharedMaterial.mainTexture = textureLeft;
		}
		transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
	}
}
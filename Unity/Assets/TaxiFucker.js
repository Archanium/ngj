#pragma strict
var player : GameObject;
var startX : float;
var xPos : float;
var chaseRange : float;
var rotation : float;

function Start () {
	player = GameObject.Find("player");
	startX = transform.position.x;
}

function Update () {
	if (player.transform.position.x > 950 && player.transform.position.x < 2880) {
		if (player.transform.position.x > transform.position.x) {
			chaseRange = chaseRange + 4;
			rotation = 180;
			if (chaseRange > 400) {
				chaseRange = 400;
			}
			if (Mathf.Abs(player.transform.position.x - transform.position.x) < 80) {
				chaseRange = chaseRange - 4;
			};
		} else {
			chaseRange = chaseRange - 4;
			rotation = 0;
			if (chaseRange < -100) {
				chaseRange = -100;
			}
			if (Mathf.Abs(player.transform.position.x - transform.position.x) < 80) {
				chaseRange = chaseRange + 4;
			};
		}
		if (Time.frameCount % 60 == 0) {
			if (Random.Range(0,10) == 2) {
				audio.Play();
			}
		}
		audio.pitch = Random.Range(0.7, 1.3);
		
		xPos = startX + chaseRange;
		transform.rotation = Quaternion.Euler(0,rotation,0);
		transform.position = Vector3(xPos,transform.position.y,transform.position.z);
	}
}
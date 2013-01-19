using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		// Backround Box (Main Menu)
		GUI.Box (new UnityEngine.Rect((Screen.width-200)/2, (Screen.height-125)/2, 200, 125), "Gab the Mind");
		bool start = GUI.Button (new UnityEngine.Rect ((Screen.width-150)/2, (Screen.height-125)/2+40, 150, 30), "Start the game");
		bool quit = GUI.Button (new UnityEngine.Rect ((Screen.width-150)/2, (Screen.height-125)/2+80, 150, 30), "Quit");
		
		// Loads Scene 1 (in your build settings)
		if (start) {
    		Application.LoadLevel (1);
		} 
		
		if (quit) {
			Application.Quit();
		}
	}
}

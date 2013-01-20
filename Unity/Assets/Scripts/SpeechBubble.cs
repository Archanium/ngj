using UnityEngine;
using System.Collections;
using System.Xml;

[ExecuteInEditMode]
public class SpeechBubble : MonoBehaviour
{
	//this game object's transform
	private Transform goTransform;
	//the game object's position on the screen, in pixels
	private Vector3 goScreenPos;
	//the game objects position on the screen
	private Vector3 goViewportPos;

	//the width of the speech bubble
	public int bubbleWidth = 200;
	//the height of the speech bubble
	public int bubbleHeight = 100;

	//an offset, to better position the bubble
	public float offsetX = 0;
	public float offsetY = 150;
	//an offset for the triangle, to better position the bubble
	public float triangleOffsetX = 0;
	public float triangleOffsetY = 0;
	public float triangleWidth = 60;
	public float triangleHeight = 150;
	
	//an offset to center the bubble
	private int centerOffsetX;
	private int centerOffsetY;

	//a material to render the triangular part of the speech balloon
	public Material mat;
	//a guiSkin, to render the round part of the speech balloon
	public GUISkin guiSkin;
	
	private bool show = false;
	private bool first = true;
	private string text;
	private XmlDocument xmlDoc;
	private XmlNodeList currentOptions;
	private float time = -4;
	public TextAsset xmlDefinition;
	private string newScene;
	

	//use this for early initialization
	void Awake ()
	{
		//get this game object's transform
		goTransform = this.GetComponent<Transform>();
	}

	//use this for initialization
	void Start()
	{
		//if the material hasn't been found
		if (!mat)
		{
			Debug.LogError("Please assign a material on the Inspector.");
			return;
		}

		//if the guiSkin hasn't been found
		if (!guiSkin)
		{
			Debug.LogError("Please assign a GUI Skin on the Inspector.");
			return;
		}
		
		// We need to have an xmlDefinition
		if (!xmlDefinition)
		{
			Debug.LogError("Please assign an XML Definition on the Inspector.");
			return;
		}
		

		//Calculate the X and Y offsets to center the speech balloon exactly on the center of the game object
		centerOffsetX = bubbleWidth/2;
		centerOffsetY = bubbleHeight/2;
	}

	//Called once per frame, after the update
	void LateUpdate()
	{
		//find out the position on the screen of this game object
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	

		//Could have used the following line, instead of lines 70 and 71
		goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);
		//goViewportPos.x = goScreenPos.x/(float)Screen.width;
		//goViewportPos.y = goScreenPos.y/(float)Screen.height;
	}

	//Draw GUIs
	void OnGUI()
	{
		if(show || (time + 3) > Time.time) {
			//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset
			GUILayout.BeginArea(new Rect(goScreenPos.x-centerOffsetX-offsetX,Screen.height-goScreenPos.y-centerOffsetY-offsetY,bubbleWidth,bubbleHeight), guiSkin.customStyles[0]);
				GUILayout.BeginVertical();
				//Render the text
				GUILayout.Label(this.text, guiSkin.label);
				for (int i = 0; i < this.currentOptions.Count; i++) {
					if(GUILayout.Button((i+1).ToString() + ". " + currentOptions[i].InnerText.Replace("#","\n"), guiSkin.button, GUILayout.Width ((bubbleWidth-10)))) {
						first = false;
						var scene = currentOptions[i].Attributes.GetNamedItem("scene");
						var response = currentOptions[i].Attributes.GetNamedItem("response");
						if(response != null) {
							this.text = response.InnerText;
							this.currentOptions = new EmptyNodeList();
							time = Time.time;
							OnGUI();
						}
						if(scene != null) {
							this.newScene = scene.InnerText;
						}
						show = false;
					};
				}
				if (this.currentOptions.Count == 0) {
					if (time == -4) {
						time = Time.time;
					} else if((time + 3) < Time.time) {
						show = false;
						first = false;
						time = -4;
					} 
				}
				GUILayout.EndVertical();
			GUILayout.EndArea();
			
		} else {
			if(!string.IsNullOrEmpty (this.newScene)) {
				EventManager.instance.QueueEvent(new SceneChangeEvent(this.newScene));
				this.newScene = null;
			}
			time = -4;
		}
	}
	void OnMouseDown() 
	{
		this.show = !this.show;
		LoadData();
	}
	
	private void LoadData()
	{
		if(show) {
			if (xmlDoc == null) {
				xmlDoc = new XmlDocument();
        		xmlDoc.LoadXml(this.xmlDefinition.text);
			}
			
			XmlNodeList dialogs = xmlDoc.SelectNodes("//dialogs/dialog[not(@first='true')]");
			XmlNode firstDialog = xmlDoc.SelectSingleNode("//dialogs/dialog[@first='true']");
			string selectString = "./text";
			int rand = Random.Range(0, dialogs.Count-1);
			XmlNode doc;
			if(first && firstDialog != null) {
				doc = firstDialog;
			} else if (dialogs.Count != 0) {
				doc = dialogs[rand];
			} else {
				this.show = false;
				return;
			}
			
			XmlNode textNode = doc.SelectSingleNode(selectString);
			this.text = textNode.InnerText;
			this.currentOptions = doc.SelectNodes("./options/option");
		}	
	}
	
	void OnTriggerEnter(Collider hit) 
	{
		this.show = true;
		this.LoadData();
	}
	
	void OnTriggerExit(Collider hit)
	{
		if(this.first) {
			this.show = false;		
		}
		
	}
}
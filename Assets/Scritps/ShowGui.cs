using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using InControl;
using AxisActions;

public class ShowGui : MonoBehaviour {
	public bool showUI;
	Rect windowRect;
	Renderer cachedRenderer;
    PlayerActions playerActions;
	string SaveData;

	void OnEnable(){
		playerActions = PlayerActions.CreateWithDefaultBindings();
		LoadBindings();
	}

	void OnDisable(){
		playerActions.Destroy ();
	}
	void Start()
	{

	}

	void Update(){
		//transform.Rotate( Vector3.down, 500.0f * Time.deltaTime * playerActions.Move.X, Space.World );
		//transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * playerActions.Move.Y, Space.World );
		if (Input.GetKeyDown (KeyCode.L)) {
			showUI = !showUI;
		}
	}


	void SaveBindings()
	{
		SaveData = playerActions.Save();
		PlayerPrefs.SetString( "Bindings", SaveData );
	}


	void LoadBindings()
	{
		if (PlayerPrefs.HasKey( "Bindings" ))
		{
			SaveData = PlayerPrefs.GetString( "Bindings" );
			playerActions.Load( SaveData );
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.Save();
	}

	float BoxHeight = 0.0f;
	void OnGUI(){
		const float h = 25f;
		var y = 10.0f;
		if (showUI == true) {
			GUI.color = Color.white;
			if (BoxHeight != 0.0f) {
				GUI.Box (new Rect(0,0,350,BoxHeight+h),"    ");
			}

			GUI.Label (new Rect(20,y,300,y+h), "Last Input Type:"+ playerActions.LastInputType.ToString());
			y += h;
			var actionCount = playerActions.Actions.Count;
			for (int i = 0; i < actionCount; i++) {
				var action = playerActions.Actions[i];
				var name = action.Name;
				if (action.IsListeningForBinding) {
					name += "(listening)";
				}

				GUI.Label (new Rect (20, y, 300, y + h), name);
				y += h;

				var bindingCount = action.Bindings.Count;
				for (int j = 0; j < bindingCount; j++)
				{
					var binding = action.Bindings [j];
					GUI.Label (new Rect (65, y, 300, y + h), binding.DeviceName +":"+binding.Name);

					if (GUI.Button (new Rect (15, y + 3.0f, 20, h - 2.5f), "-")) {
						action.RemoveBinding( binding );
					}
					if (GUI.Button (new Rect (40, y + 3.0f, 20, h - 2.5f), "+")) {
						action.ListenForBindingReplacing( binding );
					}
					y += h;
				}
				if (GUI.Button (new Rect (40, y + 3.0f, 20, h - 2.5f), "+")) {
					action.ListenForBinding();
				}
				if (GUI.Button (new Rect (65, y + 3.0f, 100, h - 2.5f), "RESET")) {
					action.ResetBindings();
				}
				y += 35.0f;
			}
			if (GUI.Button (new Rect (15, y + 3.0f, 50, h), "Load")) {
				LoadBindings();
			}
			if (GUI.Button (new Rect (75, y + 3.0f, 50, h), "Save")) {
				SaveBindings();
			}
			if (GUI.Button (new Rect (135, y + 3.0f, 50, h), "Reset")) {
				playerActions.Reset();
			}
			y += h;
			BoxHeight = y;
			//GUI.Box (new Rect(0,0,350,y+h),"    ");
		}

	}

}

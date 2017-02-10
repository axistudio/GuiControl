using System;
using UnityEngine;
using InControl;


namespace BasicExample
{
	public class BasicExample : MonoBehaviour
	{  

		public Transform gameObject1;
		public Transform gameObject2;
		void Update()
		{
			// Use last device which provided input.
			var inputDevice = InputManager.ActiveDevice;

			// Rotate target object with left stick.
			gameObject1.Rotate( Vector3.down, 500.0f * Time.deltaTime * inputDevice.LeftStickX, Space.World );
			gameObject1.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.LeftStickY, Space.World );

			// Get two colors based on two action buttons.
			var color1 = inputDevice.Action1.IsPressed ? Color.red : Color.white;
			var color2 = inputDevice.Action2.IsPressed ? Color.green : Color.white;


			gameObject2.Rotate( Vector3.down, 500.0f * Time.deltaTime * inputDevice.RightStickX, Space.World );
			gameObject2.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.RightStickY, Space.World );
	


			// Blend the two colors together to color the object.
			GetComponent<Renderer>().material.color = Color.Lerp( color1, color2, 0.5f );
		}
	}
}


﻿using InControl;
using System;
using System.Collections;
using UnityEngine;

// This custom profile is enabled by adding it to the Custom Profiles list
// on the InControlManager component, or you can attach it yourself like so:
// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
// 
public class KeyboardAndMouseProfile : UnityInputDeviceProfile
{
	public KeyboardAndMouseProfile()
	{
		Name = "Keyboard/Mouse";
		Meta = "A keyboard and mouse combination profile appropriate for FPS.";

		// This profile only works on desktops.
		SupportedPlatforms = new[]
		{
			"Windows",
			"Mac",
			"Linux"
		};

		Sensitivity = 1.0f;
		LowerDeadZone = 0.0f;
		UpperDeadZone = 1.0f;

		ButtonMappings = new[]
		{
			new InputControlMapping
			{
				Handle = "Jump",
				Target = InputControlType.Action1,
				Source = KeyCodeButton( KeyCode.Space )
			},
			new InputControlMapping
			{
				Handle = "Dash with left Shift",
				Target = InputControlType.Action3,
				Source = KeyCodeButton( KeyCode.LeftShift )
			},
			new InputControlMapping
			{
				Handle = "Dash with Right Shift",
				Target = InputControlType.Action3,
				Source = KeyCodeButton( KeyCode.RightShift )
			},
			new InputControlMapping
			{
				Handle = "Back to Menu",
				Target = InputControlType.Action4,
				Source = KeyCodeButton( KeyCode.Escape )
			}
		};

		AnalogMappings = new[]
		{
			new InputControlMapping {
				Handle = "Move X with arrow keys",
				Target = InputControlType.LeftStickX,
				Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
			},
			new InputControlMapping {
				Handle = "Move X with AD",
				Target = InputControlType.LeftStickX,
				Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
			},
			new InputControlMapping {
				Handle = "Move Y with arrow keys",
				Target = InputControlType.LeftStickY,
				Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
			},
			new InputControlMapping {
				Handle = "Move Y with WS",
				Target = InputControlType.LeftStickY,
				Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
			}
		};
	}
}
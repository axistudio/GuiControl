using System;
using InControl;
using UnityEngine;


namespace AxisActions
{
	public class PlayerActions : PlayerActionSet
	{
		public PlayerAction Trangle;
		public PlayerAction Square;
		public PlayerAction Circle;
		public PlayerAction Cross;

		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Up;
		public PlayerAction Down;
		public PlayerTwoAxisAction Move;

		public PlayerActions()
		{
			Trangle = CreatePlayerAction ("Trangle");
			Square = CreatePlayerAction ("Square");
			Circle = CreatePlayerAction ("Circle");
			Cross = CreatePlayerAction ("Cross");
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Up = CreatePlayerAction( "Move Up" );
			Down = CreatePlayerAction( "Move Down" );
			Move = CreateTwoAxisPlayerAction( Left, Right, Down, Up );
		}

		public static PlayerActions CreateWithDefaultBindings()
		{
			var playAxrions = new PlayerActions();

			playAxrions.Trangle.AddDefaultBinding(Key.A);
			playAxrions.Trangle.AddDefaultBinding (InputControlType.Action1);
			playAxrions.Trangle.AddDefaultBinding(Mouse.LeftButton);

			playAxrions.Square.AddDefaultBinding(Key.Space);
			playAxrions.Square.AddDefaultBinding (InputControlType.Action3);
			playAxrions.Square.AddDefaultBinding (InputControlType.Back);


			playAxrions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			playAxrions.Right.AddDefaultBinding( InputControlType.LeftStickRight );
			playAxrions.Up.AddDefaultBinding( InputControlType.LeftStickUp );
			playAxrions.Down.AddDefaultBinding( InputControlType.LeftStickDown );

			playAxrions.ListenOptions.IncludeUnknownControllers = true;
			playAxrions.ListenOptions.MaxAllowedBindings = 4;

			playAxrions.ListenOptions.OnBindingFound = ( action, binding ) =>
			{
				if (binding == new KeyBindingSource( Key.Escape ))
				{
					action.StopListeningForBinding();
					return false;
				}
				return true;
			};

			playAxrions.ListenOptions.OnBindingAdded += ( action, binding ) =>
			{
				Debug.Log( "Binding added... " + binding.DeviceName + ": " + binding.Name );
			};

			playAxrions.ListenOptions.OnBindingRejected += ( action, binding, reason ) =>
			{
				Debug.Log( "Binding rejected... " + reason );
			};
			return playAxrions;

		}

	}

}

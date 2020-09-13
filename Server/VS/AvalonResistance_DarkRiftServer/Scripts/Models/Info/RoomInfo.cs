using System;
using System.Collections.Generic;
using System.Text;

namespace AvalonServerPlugin.Scripts.Models.Info
{
	public class RoomInfo
	{
		public string name;
		public string description;
		public RoomAvailability availability;
		// Game settings
		public GameSettingsInfo gameSettings;
	}
}

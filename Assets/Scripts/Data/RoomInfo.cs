using System;

namespace AvalonResistance
{
	[Serializable]
	public class RoomInfo
	{
		public readonly int ID;
		public string name;
		public string description;
		public string password;

		// Room State - InGame or Waiting
		// Room Availability - Public or Private

		public RoomInfo (int roomID)
		{
			ID = roomID;
		}
	}
}
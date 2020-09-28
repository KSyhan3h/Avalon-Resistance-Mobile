using System.Collections.Generic;

namespace AvalonServerPlugin.Scripts
{
	public class Room
	{
		public static ushort GlobalID = 0;
		
		public readonly int ID;             // Unique identifier
		public ushort hostID;				// ID of the current host of the room
		public List<ushort> players;       // PlayerIDs of Players in the room
		public StateTag state;
		public RoomInfo info;                   // Room Settings set by players

		public Room (RoomInfo roomInfo)
		{
			ID = ++GlobalID;

			players = new List<ushort> ();
			state = StateTag.Open;
			info = roomInfo;
		}
	}
}

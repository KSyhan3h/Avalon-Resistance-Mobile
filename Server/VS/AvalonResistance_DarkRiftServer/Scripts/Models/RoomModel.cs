using System.Collections.Generic;
using AvalonServerPlugin.Scripts.Models.Info;

namespace AvalonServerPlugin.Scripts.Models
{
	public class RoomModel
	{
		public static ushort GlobalID = 0;
		
		public readonly int ID;             // Unique identifier
		public ushort hostID;				// ID of the current host of the room
		public List<ushort> players;       // PlayerIDs of Players in the room
		public RoomState state;
		public RoomInfo info;                   // Room Settings set by players

		public RoomModel (RoomInfo roomInfo)
		{
			ID = ++GlobalID;

			players = new List<ushort> ();
			state = RoomState.Open;
			info = roomInfo;
		}
	}
}

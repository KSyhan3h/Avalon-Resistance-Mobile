using DarkRift.Server;

namespace AvalonServerPlugin.Scripts
{
	public class Player
	{
		public const int NO_ROOM = -1;

		public readonly IClient client;
		public PlayerInfo info;
		public int roomID;

		public Player (IClient client, int roomID = NO_ROOM, PlayerInfo info = null) 
		{
			this.client = client;
			this.roomID = roomID;
			this.info = info;
		}
	}
}

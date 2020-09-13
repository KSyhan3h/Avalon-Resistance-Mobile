using DarkRift.Server;
using AvalonServerPlugin.Scripts.Models.Info;

namespace AvalonServerPlugin.Scripts.Models
{
	public class PlayerModel
	{
		public const int NO_ROOM = -1;

		public readonly IClient client;
		public int roomID;
		public PlayerInfo info;

		public PlayerModel (IClient client, int roomID = NO_ROOM, PlayerInfo info = null) 
		{
			this.client = client;
			this.roomID = roomID;
			this.info = info;
		}
	}
}

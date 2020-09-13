using UnityEngine;
using AvalonResistance.Data;

namespace AvalonResistance
{
	[CreateAssetMenu(fileName="Faction", menuName="Avalon/Settings")]
	public class Settings : ScriptableObject
	{
		
		public PlayerProfile userData;
		public Sprite card_backface;
		// Local Player data
		// Volume and stuff

		public static void SaveSettings (Settings settings) 
		{
			// Save in local 
		}

		public static void LoadSettings (Settings settings) 
		{
			// Load from local
		}
	}
}
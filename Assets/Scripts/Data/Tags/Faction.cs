using UnityEngine;

namespace AvalonResistance
{
	[CreateAssetMenu (fileName = "Faction", menuName = "Avalon/Faction")]
	public class Faction : Tag
	{
		public Character[] characters;  // Characters under the said faction

		// Conditions to Win
	}
}
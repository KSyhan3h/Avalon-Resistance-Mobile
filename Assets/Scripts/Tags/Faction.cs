using UnityEngine;

[CreateAssetMenu(fileName="Faction", menuName="Avalon/Faction")]
public class Faction : Tag 
{
	public Sprite image;			// Faction logo
	public Character[] characters;	// Characters under the said faction
}
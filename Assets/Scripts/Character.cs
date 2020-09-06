using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Avalon/Character")]
public class Character : ScriptableObject
{
	public new string name;
	public string description;
	public bool singleCard;
	public bool optional;
	public Sprite[] sprites;		
	public Faction[] factions;	
	public Trait[] traits;
}
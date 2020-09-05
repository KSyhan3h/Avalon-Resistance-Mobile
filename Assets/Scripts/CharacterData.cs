using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Avalon/CharacterData")]
public class Character : ScriptableObject
{
	public new string name;
	public string description;
	public Sprite sprite;		
	public Faction[] factions;	
	public Trait[] traits;
}
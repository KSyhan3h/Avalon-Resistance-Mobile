using UnityEngine;

namespace AvalonResistance
{
    [CreateAssetMenu (fileName = "Trait", menuName = "Avalon/Tags/Trait")]
    public class Trait : Tag
    {
        public Character[] characters;
        public Faction[] factions;
    }
}
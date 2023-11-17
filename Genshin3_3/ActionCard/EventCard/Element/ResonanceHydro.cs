using TCGBase;

namespace Genshin3_3
{
    public class ResonanceHydro : AbstractCardEvent
    {
        public override int[] Costs => new int[] { 0, 0, 1 };

        public override string NameID => "element_resonance_hydro";

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Heal(this, new DamageVariable(0, 2), new DamageVariable(0, 1, 0, true));
        }
        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Hydro).Count() >= 2;
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs) => me.Characters.Where(c => c.Alive).Any(c => c.HP < c.Card.MaxHP);
    }
}

using TCGBase;

namespace Genshin3_3
{
    public class ResonanceElectro : AbstractCardEvent
    {
        public override CostInit Cost => new CostCreate().Electro(1).ToCostInit();

        public override string NameID => "element_resonance_electro";

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var l = me.Characters.Length;
            for (int i = 0; i < l; i++)
            {
                var c = me.Characters[(i + me.CurrCharacter) % l];
                if (c.Alive && c.MP < c.Card.MaxMP)
                {
                    c.MP++;
                    break;
                }
            }
        }
        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Electro).Count() >= 2;
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs) => me.Characters.Where(c => c.Alive).Any(c => c.MP < c.Card.MaxMP);
    }
}

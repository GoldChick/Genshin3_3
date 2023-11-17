using TCGBase;

namespace Genshin3_3
{
    public class ResonanceAnemo : AbstractCardEvent, ITargetSelector
    {
        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 0, 0, 1 };

        public override string NameID => "element_resonance_anemo";

        public TargetEnum[] TargetEnums => new TargetEnum[] { TargetEnum.Character_Me };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.SwitchToIndex(targetArgs[0]);
        }
        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Anemo).Count() >= 2;
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs) => me.CurrCharacter != targetArgs[0];
    }
}

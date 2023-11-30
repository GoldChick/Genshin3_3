using TCGBase;

namespace Genshin3_3
{
    public class ResonanceAnemo : AbstractCardEvent
    {
        public override CostInit Cost => new CostCreate().Anemo(1).ToCostInit();

        public override string NameID => "element_resonance_anemo";
        public override TargetDemand[] TargetDemands => new TargetDemand[]
        {
            new(TargetEnum.Character_Me,(me,ts)=>me.CurrCharacter != ts[0] && me.Characters[ts[0]].Alive)
        };

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.SwitchToIndex(targetArgs[0]);
        }
        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Anemo).Count() >= 2;
    }
}

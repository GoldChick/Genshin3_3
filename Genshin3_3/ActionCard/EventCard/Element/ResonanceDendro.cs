using TCGBase;

namespace Genshin3_3
{
    public class ResonanceDendro : AbstractCardEventSingleEffect
    {
        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 0, 1 };

        public override string NameID => "element_resonance_dendro";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Geo).Count() >= 2;
        public override int MaxUseTimes => 1;

        //TODO:草共鸣反应加伤
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
        };
    }
}

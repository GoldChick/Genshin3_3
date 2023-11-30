using TCGBase;

namespace Genshin3_3
{
    public class ResonanceGeo : AbstractCardEventSingleEffect
    {
        public override CostInit Cost => new CostCreate().Geo(1).ToCostInit();

        public override string NameID => "element_resonance_geo";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Geo).Count() >= 2;
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--},
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (me.TeamIndex!=s.TeamID && s is HurtSender hs && hs.DirectSource is DamageSource.Character && hs.Element==5)
                {
                    var shield=me.Effects.Find(typeof(AbstractPersistentShieldYellow));
                    if (shield!=null && shield.Active)
                    {
                        shield.AvailableTimes+=3;
                        p.AvailableTimes--;
                    }
                }
            }
            }
        };
    }
}

using TCGBase;

namespace Genshin3_3
{
    public class ResonanceGeo : AbstractCardEventSingleEffect
    {
        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 1 };

        public override string NameID => "element_resonance_geo";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Geo).Count() >= 2;
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (me.TeamIndex!=s.TeamID && s is HurtSender hs && hs.DirectSource is DamageSource.Character && hs.Element==5)
                {
                    //TODO:岩共鸣
                    //var shield=me.Effects.Find("minecraft",PersistentTextures.Shield_Yellow);
                    //if (shield!=null && shield.Active)
                    //{
                    //    shield.AvailableTimes+=3;
                    //    p.AvailableTimes--;
                    //}
                }
            }
            }
        };
    }
}

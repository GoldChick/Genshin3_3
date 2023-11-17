using TCGBase;

namespace Genshin3_3
{
    public class ResonancePyro : AbstractCardEventSingleEffect
    {
        public override int[] Costs => new int[] { 0, 0, 0, 1 };

        public override string NameID => "element_resonance_pyro";

        public override bool CanBeArmed(List<AbstractCardCharacter> chars) => chars.Where(c => c.CharacterElement == ElementCategory.Pyro).Count() >= 2;
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                //该角色下一次“引发”火元素相关反应时伤害+3
                if (s is PreHurtSender phs && phs.RootSource is AbstractCardSkill skill && me.Characters[p.PersistentRegion].Card.Skills.Contains(skill) && v is DamageVariable dv)
                {
                    //if (dv.Reaction==ReactionTags.Melt || dv.Reaction==ReactionTags.Vaporize || dv.Reaction==ReactionTags.Burning ||dv.Reaction==ReactionTags.Overloaded || (dv.Reaction==ReactionTags.Crystallize && dv.Element))
                    {
                        //TODO:
                    }
                    dv.Damage+=3;
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}

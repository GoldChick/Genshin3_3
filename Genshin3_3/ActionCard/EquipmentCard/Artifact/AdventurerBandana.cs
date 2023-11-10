using TCGBase;

namespace Genshin3_3
{
    public class AdventurerBandana:AbstractCardArtifact
    {
        public override AbstractCardPersistentArtifact Effect => new E();

        public override int[] Costs => new int[] { 1 };

        public override string NameID => "artifact_adventurerbandana";
        private class E : AbstractCardPersistentArtifact
        {
            public override int MaxUseTimes => 3;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=3},
                { SenderTag.AfterUseSkill,(me,p,s,v)=>
                {
                    if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.CharIndex==p.PersistentRegion && ss.Skill.Category==SkillCategory.A)
                    {
                        me.Heal(this,new DamageVariable(0,2,p.PersistentRegion-ss.CharIndex));
                        p.AvailableTimes--;
                    }
                }
                }
            };
        }
    }
}

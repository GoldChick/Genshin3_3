using TCGBase;

namespace Genshin3_3
{
    public class LuckyDogSilverCirclet : AbstractCardArtifact
    {
        public override AbstractCardPersistentArtifact Effect => new E();

        public override int[] Costs => new int[] { 2 };
        public override bool CostSame => false;
        public override string NameID => "artifact_luckydogsilvercirclet";
        private class E : AbstractCardPersistentArtifact
        {
            public override int MaxUseTimes => 1;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1},
                { SenderTag.AfterUseSkill,(me,p,s,v)=>
                {
                    if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.CharIndex==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
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

using TCGBase;

namespace Genshin3_3
{
    public class ExileCirclet : AbstractCardArtifact
    {
        public override int[] Costs => new int[] { 2 };
        public override bool CostSame => false;
        public override string NameID => "artifact_exilecirclet";
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.CharIndex==p.PersistentRegion && ss.Skill.Category==SkillCategory.Q)
                {
                    for (int i=1; i<me.Characters.Length;i++)
                    {
                        me.Characters[(i+p.PersistentRegion)%me.Characters.Length].MP++;
                    }
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}

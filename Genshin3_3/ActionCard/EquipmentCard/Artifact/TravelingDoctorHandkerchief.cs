using TCGBase;

namespace Genshin3_3
{
    public class TravelingDoctorHandkerchief : AbstractCardArtifact
    {
        public override int[] Costs => new int[] { 1 };

        public override string NameID => "artifact_travelingdoctorhandkerchief";
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.CharIndex==p.PersistentRegion && ss.Skill.Category==SkillCategory.Q)
                {
                    int i=p.PersistentRegion-ss.CharIndex;
                    me.Heal(this,new DamageVariable(0,1,i),new DamageVariable(0,1,i,true));
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}

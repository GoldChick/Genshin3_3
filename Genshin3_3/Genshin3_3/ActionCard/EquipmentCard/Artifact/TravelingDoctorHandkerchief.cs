using TCGBase;

namespace Genshin3_3
{
    public class TravelingDoctorHandkerchief : AbstractCardArtifact
    {
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override string NameID => "artifact_travelingdoctorhandkerchief";
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.Q)
                {
                    int i=p.PersistentRegion-ss.Character.Index;
                    me.Heal(this,new(1,i),new (1,i,true));
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}

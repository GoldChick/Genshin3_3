using TCGBase;

namespace Genshin3_3
{
    public class AdventurerBandana : AbstractCardArtifact
    {
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
        public override string NameID => "artifact_adventurerbandana";
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.A)
                {
                    me.Heal(this,new HealVariable(1,p.PersistentRegion-ss.Character.Index));
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}

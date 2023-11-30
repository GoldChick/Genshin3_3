using TCGBase;

namespace Genshin3_3
{
    public class LuckyDogSilverCirclet : AbstractCardArtifact
    {
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override string NameID => "artifact_luckydogsilvercirclet";
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    me.Heal(this,new HealVariable(2,p.PersistentRegion-ss.Character.Index));
                    p.AvailableTimes--;
                }
            }
            }
        };
    }
}

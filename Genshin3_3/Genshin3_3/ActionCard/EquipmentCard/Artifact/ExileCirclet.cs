using TCGBase;

namespace Genshin3_3
{
    public class ExileCirclet : AbstractCardArtifact
    {
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override string NameID => "artifact_exilecirclet";
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.Q)
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

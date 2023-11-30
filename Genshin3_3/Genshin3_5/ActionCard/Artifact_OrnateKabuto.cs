using TCGBase;

namespace Genshin3_5
{
    public class Artifact_OrnateKabuto : AbstractCardArtifact
    {
        public override int MaxUseTimes => 0;
        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Skill.Category==SkillCategory.Q && ss.Character.Index!=p.PersistentRegion)
                {
                    var c=me.Characters[p.PersistentRegion];
                    if (c.MP<c.Card.MaxMP)
                    {
                        c.MP++;
                    }
                }
            }
            }
        };
    }
}

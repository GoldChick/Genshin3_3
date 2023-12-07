using TCGBase;

namespace Genshin3_3
{
    public class InstructorCap : AbstractCardArtifact
    {
        public override CostInit Cost => new CostCreate().Void(2).ToCostInit();
        public override string NameID => "artifact_instructorcap";
        public override int MaxUseTimes => 3;
        /// <summary>
        /// 释放一次技能只能触发一次（其实不符合描述 2023/11/9）
        /// </summary>
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.BeforeUseSkill,(me,p,s,v)=> p.Data=1 },
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                //要求是角色技能引发
                if (p.AvailableTimes>0&& p.Data!=null && s is NoDamageHurtSender hs && hs.Reaction!=ReactionTags.None && hs.RootSource is AbstractCardSkill sk && me.Characters[p.PersistentRegion].Card.Skills.Contains(sk))
                {
                    p.Data=2;
                }
            }
            },
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.Data!=null && p.Data.Equals(2))
                {
                    me.AddDiceRange((int)me.Characters[p.PersistentRegion].Card.CharacterElement);
                    p.AvailableTimes--;
                }
                p.Data=null;
            }
            }
        };
    }
}

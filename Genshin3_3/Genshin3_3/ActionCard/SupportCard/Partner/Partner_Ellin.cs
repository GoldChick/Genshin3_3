using TCGBase;

namespace Genshin3_3
{
    public class Partner_Ellin : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        //TODO:艾琳计数器更新
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss)
                {
                    me.AddPersistent(new Effect_Ellin(),ss.Character.Index);
                    var ellin=ss.Character.Effects.Find(typeof(Effect_Ellin));
                    if (ellin != null)
                    {
                        if (ellin.Data is List<AbstractCardSkill> list)
                        {
                            list.Add(ss.Skill);
                        }
                        else
                        {
                            ellin.Data=new List<AbstractCardSkill>(){ ss.Skill};
                        }
                    }
                }
            }
            },
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Character.Effects.Find(typeof(Effect_Ellin)) is Persistent<ICardPersistent> icp && icp.Data is List<AbstractCardSkill> list && list.Contains(s.Skill),
                0,1)
            }
        };

    }
    public class Effect_Ellin : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes--}
        };
    }
}

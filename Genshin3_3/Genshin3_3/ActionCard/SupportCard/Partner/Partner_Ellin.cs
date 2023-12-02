using TCGBase;

namespace Genshin3_3
{
    public class Partner_Ellin : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Partner;
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.UseDiceFromSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0&&me.TeamIndex==s.TeamID && s is UseDiceFromSkillSender ss && ss.Character.Effects.Find(typeof(Effect_RoundSkillCounter)) is AbstractPersistent cnt)
                {
                    if (cnt.Data is Dictionary<AbstractCardSkill, int> map && map.ContainsKey(ss.Skill) && v is CostVariable cv)
                    {
                        CostModifier mod=new(DiceModifierType.Same,1);
                        if (mod.Modifier(cv) && ss.IsRealAction)
                        {
                            p.AvailableTimes--;
                        }
                    }
                }
            }
            }
        };
    }
}

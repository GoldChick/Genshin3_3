using TCGBase;

namespace Genshin3_7
{
    public class Location_SumeruCity : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Place;

        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromCard,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is UseDiceFromCardSender ss && ss.Card is AbstractCardEquipmentTalent)
                {
                    if (me.GetCards().Count>=me.GetDices().Sum() && v is CostVariable cv)
                    {
                        CostModifier mod=new(DiceModifierType.Same,1);
                        if (mod.Modifier(cv) && ss.IsRealAction)
                        {
                            p.AvailableTimes--;
                        }
                    }
                }
            }
            },
            { SenderTag.UseDiceFromSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0 && me.TeamIndex==s.TeamID && s is UseDiceFromSkillSender ss)
                {
                    if (me.GetCards().Count>=me.GetDices().Sum() && v is CostVariable cv)
                    {
                        CostModifier mod=new(DiceModifierType.Same,1);
                        if (mod.Modifier(cv) && ss.IsRealAction)
                        {
                            p.AvailableTimes--;
                        }
                    }
                }
            }
            },
            {
                SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes
            }
        };

    }
}

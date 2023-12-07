using TCGBase;

namespace Genshin3_7
{
    public class Event_HeavyStrike : AbstractCardEventSingleEffect
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>p.Active=false},
            { SenderTag.UseDiceFromCard,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID)
                {
                    p.Data=me.DiceNum%2==0;
                }
            }
            },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterSkill(me,p,s,v,SkillCategory.A,out var dv))
                {
                    if (true.Equals(p.Data))
                    {
                        dv.Damage++;
	                }
                    dv.Damage++;
                    p.Active=false;
	            }
            }
            }
        };

        public override CostInit Cost => new CostCreate().Same(1).ToCostInit();
    }
}

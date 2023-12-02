using TCGBase;

namespace Genshin3_7
{
    public class Item_RedFeatherFan : AbstractCardSupport
    {
        public override SupportTags SupportTag => SupportTags.Item;

        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && p.AvailableTimes>0)
                {
                    p.AvailableTimes--;
                }
            } 
            },
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes}
        };

        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
    }
    public class Effect_RedFeatherFan : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSwitch,new PersistentDiceCostModifier<UseDiceFromSwitchSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID,
                0,1,false,(me,p,s)=>p.Data=114)},
            { SenderTag.AfterSwitch,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && v is FastActionVariable fv && !fv.Fast)
                {
                    fv.Fast=true;
                    p.Data=114;
	            }
                if (p.Data!=null)
                {
                    p.Active=false;
	            }
            } 
            }
        };
    }
}

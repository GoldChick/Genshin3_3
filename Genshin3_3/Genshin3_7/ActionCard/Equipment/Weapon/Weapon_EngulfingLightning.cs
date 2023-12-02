using TCGBase;

namespace Genshin3_7
{
    internal class Weapon_EngulfingLightning:AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.AfterAnyAction,(me,p,s,v)=>
            {
                var c=me.Characters[p.PersistentRegion];
                if (p.AvailableTimes>0 && c.MP==0)
                {
                    c.MP++;
                    p.AvailableTimes--;
	            }
            } 
            },
            { SenderTag.RoundStep,(me,p,s,v)=>
            {
                p.AvailableTimes=MaxUseTimes;
            } 
            }
        };
    }
}

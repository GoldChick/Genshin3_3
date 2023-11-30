using TCGBase;

namespace Genshin3_3
{
    public class LithicWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public LithicWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_lithic_{category.ToString().ToLower()}";
        }
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.AddPersistent(new LithicWeaponShield(), targetArgs[0]);
            base.AfterUseAction(me, targetArgs);
        }

        public override int MaxUseTimes => 0;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    if (dv.Element>=0)
                    {
                        dv.Damage++;
                    }
                }
            }
            }
        };
    }
    //TODO:看场上的角色数量
    public class LithicWeaponShield : AbstractPersistentShieldYellow
    {
        public override int MaxUseTimes => 3;
    }

}

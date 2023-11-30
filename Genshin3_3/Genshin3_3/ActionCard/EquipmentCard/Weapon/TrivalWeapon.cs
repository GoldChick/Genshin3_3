using TCGBase;

namespace Genshin3_3
{
    public class TrivalWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(2).ToCostInit();
        public TrivalWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_{category.ToString().ToLower()}";
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
}

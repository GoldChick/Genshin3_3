﻿using TCGBase;

namespace Genshin3_3
{
    public class WolfGravestoneWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override AbstractCardPersistentWeapon Effect => new E();
        public override int[] Costs => new int[] { 3 };
        public WolfGravestoneWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_wolfgravestone_{category.ToString().ToLower()}";
        }
        private class E : AbstractCardPersistentWeapon
        {
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
                        if (me.Enemy.Characters[dv.TargetIndex].HP<=6)
                        {
                            dv.Damage+=2;
	                    }
                    }
                } 
                }
            };
        }
    }
}
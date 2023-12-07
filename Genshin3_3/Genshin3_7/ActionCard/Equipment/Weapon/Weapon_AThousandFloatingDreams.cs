using TCGBase;

namespace Genshin3_7
{
    partial class Weapon_AThousandFloatingDreams : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;
        public override int MaxUseTimes => 2;
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (me.TeamIndex==s.TeamID && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && v is DamageVariable dv)
                {
                    if (dv.Reaction!=ReactionTags.None && p.AvailableTimes>0)
                    {
                        //与原版不同，只要是根本来源为角色，都可以加伤
                        p.AvailableTimes--;
                        dv.Damage+=1;
                    }
                    if (me.CurrCharacter==p.PersistentRegion && dv.DirectSource==DamageSource.Character)
                    {
                        dv.Damage+=1;
                    }
                }
            }
            }
        };
    }
}

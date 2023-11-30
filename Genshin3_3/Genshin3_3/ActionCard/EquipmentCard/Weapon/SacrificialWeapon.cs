using TCGBase;

namespace Genshin3_3
{
    public class SacrificialWeapon : AbstractCardWeapon
    {
        public override string NameID { get; }
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public SacrificialWeapon(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_sacrificial_{category.ToString().ToLower()}";
        }
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>p.AvailableTimes=1 },
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
            },
            {SenderTag.AfterUseSkill, (me,p,s,v)=>
                {
                    if (me.TeamIndex==s.TeamID && p.AvailableTimes > 0 && s is AfterUseSkillSender sks && sks.Character.Index==p.PersistentRegion && sks.Skill.Category == SkillCategory.E)
                    {
                        me.AddDiceRange((int)me.Characters[p.PersistentRegion].Card.CharacterElement);
                        p.AvailableTimes--;
                    }
                }
            }
        };
    }
}

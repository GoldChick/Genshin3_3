using TCGBase;

namespace Genshin3_6
{
    public class Weapon_Favonius : AbstractCardWeapon
    {
        public override WeaponCategory WeaponCategory { get; }
        public override CostInit Cost => new CostCreate().Same(3).ToCostInit();
        public override string NameID { get; }
        public Weapon_Favonius(WeaponCategory category)
        {
            WeaponCategory = category;
            NameID = $"weapon_favonius_{category.ToString().ToLower()}";
        }
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStart,(me,p,s,v)=>p.AvailableTimes=MaxUseTimes},
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.AvailableTimes>0&&me.TeamIndex==s.TeamID && s is AfterUseSkillSender ss && ss.Character.Index==p.PersistentRegion && ss.Skill.Category==SkillCategory.E)
                {
                    var c=me.Characters[p.PersistentRegion];
                    if (c.MP<c.Card.MaxMP)
                    {
                        p.AvailableTimes--;
                        me.Characters[p.PersistentRegion].MP++;
                    }
                }
            } },
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    if (dv.Element>=0)
                    {
                        dv.Damage++;
                    }
                }
            }}
        };
    }
}

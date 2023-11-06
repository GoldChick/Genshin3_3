using System.Reflection;
using TCGBase;
 

namespace Genshin3_3
{
    public class SacrificialSword : AbstractCardWeapon
    {
        public override string NameID => "sacrificial_Sword";

        public override int[] Costs => new int[] { 1 };

        public override bool CostSame => false;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public override AbstractCardPersistentEffect Effect => new 祭礼剑_effect();

        public class 祭礼剑_effect : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 1;
            public override bool CustomDesperated => false;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.DamageIncrease.ToString(), new PersistentWeapon() },
                {SenderTag.AfterUseSkill.ToString(), (me,p,s,v)=>
                    {
                        if (p.AvailableTimes > 0 && s is AfterUseSkillSender sks && sks.Skill.Category == SkillCategory.E)
                        {
                            p.AvailableTimes--;
                        }
                    }
                },
                {SenderTag.RoundStart.ToString(), new PersistentSimpleUpdate() }
            };
        }
    }
}

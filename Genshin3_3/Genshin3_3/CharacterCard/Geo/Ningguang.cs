using TCGBase;

namespace Genshin3_3
{
    public class Ningguang : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Geo(1).ToCostInit(),new DamageVariable(5,1)),
            new CharacterEffectE(5,2,new Effect_Ningguang(),false),
            new 天权崩玉()
        };

        public override ElementCategory CharacterElement => ElementCategory.Geo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "ningguang";
        private class 天权崩玉 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override CostInit Cost => new CostCreate().Geo(3).MP(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new DamageVariable(5, me.Effects.Contains(typeof(Effect_Ningguang)) ? 8 : 6, 0), this);
            }
        }
    }
    public class Effect_Ningguang : AbstractCardPersistent
    {
        public override string NameID => "effect_ningguang";
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {SenderTag.HurtDecrease,new PersistentPurpleShield(1,2) }
        };
    }

    public class Talent_Ningguang : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "ningguang";

        public override int Skill => 1;

        public override CostInit Cost => new CostCreate().Geo(4).ToCostInit();

        public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.DamageIncrease,(me,p,s,v)=>
                    {
                        if (me.TeamIndex==s.TeamID && me.Effects.Contains(typeof(Effect_Ningguang)) &&  v is DamageVariable dv && dv.Element==5)
                        {
                            dv.Damage++;
                        }
                    }
                }
            };
    }

}

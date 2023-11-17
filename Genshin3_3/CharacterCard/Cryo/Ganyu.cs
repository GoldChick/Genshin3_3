using TCGBase;

namespace Genshin3_3
{
    /// <summary>
    /// 奥兹换班霜华矢，唯此一心冰灵珠！
    /// </summary>
    public class Ganyu : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,1,new Effect_Ganyu(),false),
            new 霜华矢(),
            new CharacterSingleSummonQ(1,2,new Summon_Ganyu()),
        };
        private class 霜华矢 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.A;

            public override int[] Costs => new int[] { 0, 5 };

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.MultiHurt(new DamageVariable[] { new(1, 2, 0), new(-1, 2, 0, true) }, this);
            }
        }

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "ganyu";
    }
    public class Effect_Ganyu : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.HurtDecrease, new PersistentPurpleShield(1)}
            };

        public override string NameID => "effect_ganyu";
    }
    public class Summon_Ganyu : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>
                    {
                        me.Enemy.MultiHurt(new DamageVariable[]{new(1,1,0),new(-1,1,0,true) },this);
                        p.AvailableTimes--;
                    }
                }
            };
        public override string NameID => "summon_ganyu";
    }
    public class Talent_Ganyu : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "ganyu";
        public override int Skill => 2;
        public override int[] Costs => new int[] { 0, 5 };
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.MultiHurt(new DamageVariable[] { new(1, 2, 0), new(-1, c.Effects.Contains("used_5a") ? 3 : 2, 0, true) }, c.Card.Skills[2]);
            //TODO:甘雨天赋
        }
    }
}

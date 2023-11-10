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
            new CharacterEffectE(1,1,new 冰莲(),false),
            new 霜华矢(),
            new CharacterSingleSummonQ(1,2,new 冰灵珠()),
        };
        private class 冰莲 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 2;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.HurtDecrease, new PersistentPurpleShield(1)}
            };

            public override string TextureNameID => PersistentTextures.Shield_Purple;
        }
        private class 冰灵珠 : AbstractCardPersistentSummon
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
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "summon_ganyu";
        }
        private class 霜华矢 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.A;

            public override int[] Costs => new int[] { 0, 5 };

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.MultiHurt(new DamageVariable[] { new(1, 2, 0), new(-1, 2, 0, true) }, this);
                me.AddPersistent(new SimpleEffect("used_5a"), c.Index);
            }
        }

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "ganyu";
    }
    public class Talent_Ganyu : AbstractCardEquipmentFightActionTalent
    {
        public override string CharacterNameID => "ganyu";

        public override int Skill => 2;

        public override CardPersistentTalent Effect => new E();

        public override int[] Costs => new int[] { 0, 5 };
        private class E : CardPersistentTalent
        {
            public override int Skill => 2;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.MultiHurt(new DamageVariable[] { new(1, 2, 0), new(-1, c.Effects.Contains("used_5a") ? 3 : 2, 0, true) }, c.Card.Skills[2]);
                me.AddPersistent(new SimpleEffect("used_5a"), c.Index);
            }
        }
    }
}

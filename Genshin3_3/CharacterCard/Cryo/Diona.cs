using TCGBase;

namespace Genshin3_3
{
    /// <summary>
    /// 奥兹换班霜华矢，唯此一心冰灵珠！
    /// </summary>
    public class Diona : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,1,new 猫爪护盾(1),false),
            new CharacterSingleSummonQ(1,2,new 酒雾领域()),
        };
        public class 猫爪护盾 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes { get; }
            public 猫爪护盾(int maxusetimes)
            {
                MaxUseTimes = maxusetimes;
            }
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.HurtDecrease, new PersistentYellowShield()}
            };

            public override string TextureNameID => PersistentTextures.Shield_Yellow;
        }
        private class 酒雾领域 : AbstractCardPersistentSummon
        {
            public override int MaxUseTimes => 2;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>
                    {
                        me.Enemy.Hurt(new(4,1),this,()=>me.Heal(this,new DamageVariable(0,2)));
                        p.AvailableTimes--;
                    }
                }
            };
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "summon_diona";
        }

        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "diona";
    }
    public class Talent_Diona : AbstractCardEquipmentFightActionTalent
    {
        public override string CharacterNameID => "diona";

        public override int Skill => 1;

        public override CardPersistentTalent Effect => new E();

        public override int[] Costs => new int[] { 0, 3 };
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            me.Effects.TryRemove(typeof(Diona.猫爪护盾));
            base.AfterUseAction(me, targetArgs);
        }
        private class E : CardPersistentTalent
        {
            public override int Skill => 1;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(4, 2), c.Card.Skills[1], ()=>me.AddPersistent(new Diona.猫爪护盾(2)));
            }
        }
    }
}

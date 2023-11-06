using TCGBase;

namespace Genshin3_3
{
    public class 讨债哥 : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,3),
            new CharacterEffectE(3,1,new 潜行()),
            new CharacterSimpleQ(3,5),
            new 被动()
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Other;

        public override CharacterRegion CharacterRegion => CharacterRegion.SNEZHNAYA;

        public override string NameID => "debt";
        public class 潜行 : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes { get; }
            public 潜行(bool talent = false)
            {
                MaxUseTimes = talent ? 3 : 2;
                TriggerDic.Add(SenderTag.ElementEnchant, (me, p, s, v) =>
                {
                    if (PersistentFunc.IsCurrCharacterDamage(me, p, s, v, out var dv))
                    {
                        if (talent)
                        {
                            dv.Element = 3;
                        }
                        dv.Damage++;
                        p.AvailableTimes--;
                    }
                });
            }

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.HurtDecrease,new PersistentPurpleShield(1,1)}
            };
        }

        private class 被动 : AbstractPassiveSkill
        {
            public override string[] TriggerDic => new string[] { SenderTag.RoundStart.ToString()};

            public override bool TriggerOnce => true;

            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                me.AddPersistent(new 潜行(), c.Index);
            }
        }
    }
    public class Talent_讨债哥 : AbstractCardTalent
    {
        public override CardPersistentTalent Effect => new Talent_E();
        public override string CharacterNameID => "debt";
        public override int Skill => 1;
        public override int[] Costs => new int[] { 0,0,0,3};
        public override string NameID => "talent_debt";
        public override void AfterUseAction(PlayerTeam me, int[]? targetArgs = null)
        {
            var p = me.Effects.Find(typeof(讨债哥.潜行));
            if (p != null)
            {
                p.Active = false;
            }
            base.AfterUseAction(me, targetArgs);
        }
        private class Talent_E: CardPersistentTalent
        {
            public override int Skill => 1;
            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                me.Enemy.Hurt(new(3, 1), c.Card.Skills[1],()=>me.AddPersistent(new 讨债哥.潜行(true),c.Index));
            }
        }
    }
}

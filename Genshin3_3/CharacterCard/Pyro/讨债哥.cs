using TCGBase;

namespace Genshin3_3
{
    public class 讨债哥 : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,3),
            new CharacterEffectE(3,1,new Effect_Debt()),
            new CharacterSimpleQ(3,5),
            new 被动()
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Other;

        public override CharacterRegion CharacterRegion => CharacterRegion.SNEZHNAYA;

        public override string NameID => "debt";

        private class 被动 : AbstractPassiveSkill
        {
            public override string[] TriggerDic => new string[] { SenderTag.RoundStart.ToString() };

            public override bool TriggerOnce => true;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.AddPersistent(new Effect_Debt(), c.Index);
            }
        }
    }
    public class Effect_Debt : AbstractCardPersistent
    {
        public override int MaxUseTimes { get; }
        public Effect_Debt(bool talent = false)
        {
            Variant = talent ? 1 : 0;
            MaxUseTimes = talent ? 3 : 2;
            TriggerDic = new()
            {
                { SenderTag.HurtDecrease, new PersistentPurpleShield(1) },
                { SenderTag.ElementEnchant, (me, p, s, v) =>
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
                }
                }
            };
        }

        public override PersistentTriggerDictionary TriggerDic { get; }
    }

    public class Talent_Debt : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "debt";
        public override int Skill => 1;
        public override int[] Costs => new int[] { 0, 0, 0, 3 };
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(3, 1), c.Card.Skills[1], () => me.AddPersistent(new Effect_Debt(true), c.Index));
        }
    }
}

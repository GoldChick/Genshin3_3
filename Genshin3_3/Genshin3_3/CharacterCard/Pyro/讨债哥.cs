using TCGBase;

namespace Genshin3_3
{
    public class 讨债哥 : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,3),
            new CharacterEffectE(3,1,new Effect_Debt_Passive()),
            new CharacterSimpleQ(3,5),
            new Effect_Debt_Passive()
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Other;

        public override CharacterRegion CharacterRegion => CharacterRegion.Fatui;

        public override string NameID => "debt";

    }
    public class Effect_Debt_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => true;
        public override int MaxUseTimes { get; }

        public Effect_Debt_Passive(bool talent = false)
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
        public override CostInit Cost => new CostCreate().Pyro(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(3, 1), c.Card.Skills[1], () => me.AddPersistent(new Effect_Debt_Passive(true), c.Index));
        }
    }
}

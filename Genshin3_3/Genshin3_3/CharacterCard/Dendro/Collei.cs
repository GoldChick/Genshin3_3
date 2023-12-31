﻿using TCGBase;

namespace Genshin3_3
{
    public class Collei : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Dendro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleSkill(SkillCategory.E,new CostCreate().Dendro(3).ToCostInit(),new DamageVariable(6,3)),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Dendro(3).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Collei()),new DamageVariable(6,2))
        };

        public override ElementCategory CharacterElement => ElementCategory.Dendro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;

        public override string NameID => "collei";
    }
    public class Summon_Collei : AbstractSimpleSummon
    {
        public Summon_Collei() : base(6, 2, 2)
        {
        }
    }

    public class Talent_Collei : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "collei";

        public override int Skill => 1;

        public override CostInit Cost => new CostCreate().Dendro(4).ToCostInit();

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepReset(),
        };
        public override int MaxUseTimes => 1;
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(6, 3), c.Card.Skills[1], () =>
            {
                var talent = c.Effects.Find(Namespace, NameID);
                if (talent != null && talent.AvailableTimes > 0)
                {
                    me.AddPersistent(new Effect_Collei());
                    talent.AvailableTimes--;
                }
            });
        }
    }
    public class Effect_Collei : AbstractCardEffect
    {
        public override int MaxUseTimes => 1;
        public override string NameID => "effect_collei";
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            new PersistentPreset.RoundStepDecrease(),
            { SenderTag.AfterHurt,(me,p,s,v)=>
            {
                if (me.TeamIndex!=s.TeamID && s is HurtSender hs && hs.RootSource is AbstractCardSkill)
                {
                    int r=(int)hs.Reaction;
                    if (r>=8 && r<=10)
                    {
                        me.Enemy.Hurt(new(6,1),this);
                        p.AvailableTimes--;
                    }
                }
            }
            }
        };
    }

}

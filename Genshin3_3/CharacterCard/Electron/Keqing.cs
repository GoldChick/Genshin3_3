﻿using TCGBase;


namespace Genshin3_3
{
    public class Talent_Special_Keqing : AbstractCardEventTalent
    {
        public override int[] Costs => new int[] { 0, 0, 0, 0, 3 };
        public override string CharacterNameID => "keqing";
        public override string NameID => "talent_keqing_e";
        public override int Skill => 1;

        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var c = me.Characters[targetArgs[0]];
            me.Game.HandleEvent(new(new(ActionType.SwitchForced, targetArgs[0])), me.TeamIndex);
            if (c.Alive && me.CurrCharacter == c.Index)
            {
                me.Game.HandleEvent(new NetEvent(new NetAction(ActionType.UseSKill, 1), Array.Empty<int>(), new int[] { 114, 514 }), me.TeamIndex);
            }
        }
        public override bool CanBeUsed(PlayerTeam me, int[] targetArgs)
        {
            var c = me.Characters[targetArgs[0]];
            var card = c.Card;
            return c.Alive && c.Active && $"{CharacterNamespace ?? Namespace}:{CharacterNameID}".Equals($"{card.Namespace}:{card.NameID}");
        }
    }

    public class Keqing : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,4),
            new 星斗归位(),
            new 天街巡游()
        };

        public override string NameID => "keqing";

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

        public class 星斗归位 : AbstractCardSkill
        {
            public override int[] Costs => new int[] { 0, 0, 0, 0, 3 };
            public override SkillCategory Category => SkillCategory.E;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(4, 3, 0), this);

                if (me.GetCards().Any(p => p is Talent_Special_Keqing))
                {
                    me.TryRemoveAllCard(c => c is Talent_Special_Keqing);
                    me.AddPersistent(new Effect_Keqing(), c.Index);
                }
                else
                {
                    me.GainCard(new Talent_Special_Keqing());
                }
            }
        }
        public class 天街巡游 : AbstractCardSkill
        {
            public override int[] Costs => new int[] { 0, 0, 0, 0, 4 };
            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.MultiHurt(new DamageVariable[]
                {
                new(4, 4) ,
                new(-1, 3,  0, true)
                }, this);
            }
        }
    }
    public class Effect_Keqing : AbstractCardPersistent
    {
        public Effect_Keqing(bool talent = false)
        {
            MaxUseTimes = talent ? 3 : 2;
            Variant = talent ? 1 : 0;
            TriggerDic = new()
            {
                { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
                { SenderTag.ElementEnchant,new PersistentElementEnchant(4,true,talent?1:0)}
            };
        }
        public override int MaxUseTimes { get; }

        public override PersistentTriggerDictionary TriggerDic { get; }
    }

    public class Talent_Keqing : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "keqing";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 3 };

        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.Hurt(new(4, 3, 0), c.Card.Skills[1]);

            if (me.GetCards().Any(p => p is Talent_Special_Keqing))
            {
                me.TryRemoveAllCard(c => c is Talent_Special_Keqing);
                me.AddPersistent(new Effect_Keqing(true), c.Index);
            }
            else
            {
                me.GainCard(new Talent_Special_Keqing());
            }
        }
    }
}

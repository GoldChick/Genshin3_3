﻿using TCGBase;

namespace Genshin3_3
{
    public class Yoimiya : AbstractCardCharacter
    {
        public override int MaxMP => 1;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,3),
            new E(),
            new CharacterEffectQ(3,3,new QEffect(),false)
        };

        public override ElementCategory CharacterElement => ElementCategory.Pyro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.INAZUMA;

        public override string NameID => "yoimiya";
        public class E : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.E;
            public override int[] Costs => new int[] { 0, 0, 0, 1 };
            public override bool GiveMP => false;
            public override void AfterUseAction(PlayerTeam me, Character c, int[]? targetArgs = null)
            {
                me.AddPersistent(new 点燃(), c.Index);
            }
            private class 点燃 : AbstractCardPersistentEffect
            {
                public override string TextureNameID => PersistentTextures.Atk_Up_Pyro;
                public override int MaxUseTimes => 2;

                public override PersistentTriggerDictionary TriggerDic => new()
                {
                    { SenderTag.ElementEnchant.ToString(),(me,p,s,v)=>
                        {
                            if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                            {
                                if (s is PreHurSender phs && phs.RootSource is AbstractCardSkill acs && acs.Category==SkillCategory.A)
                                {
                                    dv.Damage++;
                                    dv.Element=3;
                                    p.AvailableTimes--;
                                }
                            }
                        }
                    }
                };
            }
        }
        private class QEffect : AbstractCardPersistentEffect
        {
            public override int MaxUseTimes => 2;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundStart.ToString(),(me, p, s, v) => p.AvailableTimes --},
                { SenderTag.AfterUseSkill.ToString(),(me, p, s, v) => 
                {
                    if(s is UseSkillSender usks && usks.Skill.Category == SkillCategory.A) 
                    {
                        me.Enemy.Hurt(new(3, 1, 0), this); 
                    } 
                }}
            };
            public override string TextureNameSpace => "Genshin3_3";
            public override string TextureNameID => "effect_yoimiya";
        }
    }
}
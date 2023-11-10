﻿using TCGBase;

namespace Genshin3_3
{
    public class Noel : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,5),
            new CharacterEffectE(5,1,new 护心铠(),false),
            new CharacterEffectQ(5,4,new 大扫除(),true,5,4)
        };

        public override ElementCategory CharacterElement => ElementCategory.Geo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        public override string NameID => "noel";
        public class 护心铠 : AbstractCardPersistentEffect
        {
            public override string TextureNameID => PersistentTextures.Shield_Yellow;
            public override int MaxUseTimes => 2;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.HurtDecrease,new PersistentYellowShield()},
                {
                    SenderTag.HurtMul,(me, p, s, v) =>
                    {
                        if (me.TeamIndex == s.TeamID && v is DamageVariable dv && dv.TargetIndex == me.CurrCharacter)
                        {
                            dv.Damage = (dv.Damage + 1) / 2;
                        }
                    }
                },
            };
        }
        private class 大扫除 : AbstractCardPersistentEffect
        {
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "effect_noel";
            public override int MaxUseTimes => 2;

            public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.AfterAnyAction,(me,p,s,v)=>
                    {
                        if (p.Data==null)
                        {
                            p.Data=1;
                            me.AddPersistent(new EffectDice(),p.PersistentRegion,p) ;
                        }
                    }
                },
                {SenderTag.ElementEnchant,new PersistentElementEnchant(5,false,2)},
                {SenderTag.RoundOver,(me,p,s,v)=> {p.AvailableTimes--; p.Data=null; } }
            };
            private class EffectDice : AbstractCardPersistentEffect
            {
                public override int MaxUseTimes => 1;

                public override PersistentTriggerDictionary TriggerDic => new()
                {
                    {SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                        (me,p,s,v)=>  s.ChaIndex==p.PersistentRegion && s.SkillIndex==0
                        ,5,1
                     )},
                    {SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--}
                };
            }
        }
    }
    public class Talent_Noel : AbstractCardEquipmentFightActionTalent
    {
        public override string CharacterNameID => "noel";

        public override int Skill => 1;

        public override CardPersistentTalent Effect => new E();

        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 3 };
        private class E : CardPersistentTalent
        {
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.RoundOver, (me, p, s, v) => p.Data = null},
                { SenderTag.AfterUseSkill, (me, p, s, v) =>
                    {
                        if (s is AfterUseSkillSender ss && ss.CharIndex==p.PersistentRegion && ss.Skill.Category==SkillCategory.A)
                        {
                            if (p.Data==null && me.Effects.Contains(typeof(Noel.护心铠)) && me.Characters[p.PersistentRegion].Alive)
                            {
                                me.Heal(this,new DamageVariable(0,1),new DamageVariable(0,1,0,true));
                                p.Data=1;
                            }
                        }
                    }
                }
            };
        }
    }

}

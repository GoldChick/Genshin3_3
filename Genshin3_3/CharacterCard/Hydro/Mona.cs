using TCGBase;

namespace Genshin3_3
{
    public class Mona : AbstractCardCharacter
    {
        public override int MaxMP => 3;

        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(2,1),
            new CharacterSingleSummonE(2,1,new 虚影()),
            new CharacterEffectQ(2,4,new 泡影(),false)
        };

        public override string NameID => "mona";

        public override ElementCategory CharacterElement => ElementCategory.Hydro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Catalyst;

        public override CharacterRegion CharacterRegion => CharacterRegion.MONDSTADT;

        private class 虚影 : AbstractCardPersistentSummon
        {
            public override bool CustomDesperated => true;
            public override int MaxUseTimes => 1;
            public override PersistentTriggerDictionary TriggerDic => new() {
                { SenderTag.HurtDecrease, new PersistentPurpleShield(1) },
                { SenderTag.RoundOver, (me,p,s,v)=>
                {
                    me.Enemy.Hurt(new(2, 1,  0),this);
                    p.Active = false;
                }}
            };

            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "summon_mona";
        }
        private class 泡影 : AbstractCardPersistentEffect
        {
            public override string TextureNameSpace => "genshin3_3";
            public override string TextureNameID => "effect_mona";
            public override int MaxUseTimes => 1;
            public override PersistentTriggerDictionary TriggerDic => new()
            {
                { SenderTag.HurtMul,(me,p,s,v)=>
                    {
                        if (me.TeamIndex==s.TeamID && v is DamageVariable dv)
                        {
                            dv.Damage *= 2;
                            p.AvailableTimes--;
                        }
                    }
                }
            };
        }
        private class 虚实流动 : AbstractPassiveSkill
        {
            public override string[] TriggerDic => throw new NotImplementedException();

            public override bool TriggerOnce => throw new NotImplementedException();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                throw new NotImplementedException();
            }
        }
    }
}

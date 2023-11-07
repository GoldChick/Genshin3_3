using TCGBase;
namespace Genshin3_3
{
    public class 丘丘岩盔王 : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,5),
            new CharacterSimpleE(0,3,5),
            new CharacterSimpleQ(0,5,5),
            new Passive()
        };
        public override ElementCategory CharacterElement => ElementCategory.Geo;
        public override WeaponCategory WeaponCategory => WeaponCategory.Other;
        public override CharacterRegion CharacterRegion => CharacterRegion.QQ;
        public override CharacterCategory CharacterCategory => CharacterCategory.Mob;
        public override string NameID => "qq";
        private class Passive : AbstractPassiveSkill
        {
            public override string[] TriggerDic => new string[] { SenderTag.GameStart.ToString() };

            public override bool TriggerOnce => true;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.AddPersistent(new 岩盔(), c.Index);
            }
            private class 岩盔 : AbstractCardPersistentEffect
            {
                public override int MaxUseTimes => 3;

                public override PersistentTriggerDictionary TriggerDic => new()
                {
                    {SenderTag.HurtDecrease.ToString(),new PersistentPurpleShield(1) },
                    {SenderTag.RoundStart.ToString(),(me, p, s, v) => me.AddPersistent(new 坚岩之力(), p.PersistentRegion, p)}
                };
                public override string TextureNameID => PersistentTextures.Shield_Purple;
            }
            private class 坚岩之力 : AbstractCardPersistentEffect
            {
                public override string TextureNameID => PersistentTextures.Atk_Up;
                public override int MaxUseTimes => 1;
                public override PersistentTriggerDictionary TriggerDic => new()
                {
                    { SenderTag.ElementEnchant.ToString(),(me,p,s,v)=>
                        {
                            if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                            {
                                dv.Element=5;
                                dv.Damage++;
                                p.AvailableTimes--;
                            }
                        }
                    }
                };

            }
        }
    }
}

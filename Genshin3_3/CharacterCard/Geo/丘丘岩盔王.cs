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
                me.AddPersistent(new Effect_QQ_DEF(), c.Index);
            }
        }
    }
    public class Effect_QQ_DEF : AbstractCardPersistent
    {
        public override int MaxUseTimes => 3;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            {SenderTag.HurtDecrease,new PersistentPurpleShield(1) },
            {SenderTag.RoundStart,(me, p, s, v) => me.AddPersistent(new Effect_QQ_ATK(), p.PersistentRegion, p)}
        };
    }
    public class Effect_QQ_ATK : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.ElementEnchant,(me,p,s,v)=>
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
    public class Talent_丘丘岩盔王 : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "qq";

        public override int Skill => 2;

        public override int[] Costs => new int[] { 0, 0, 0, 0, 0, 4 };
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.Die,(me,p,s,v)=>
                {
                    if (me.TeamIndex!=s.TeamID)
                    {
                        me.AddPersistent(new Effect_QQ_DEF(), p.PersistentRegion);
                        me.AddPersistent(new Effect_QQ_ATK(), p.PersistentRegion, me.Characters[p.PersistentRegion].Effects.Find(typeof(Effect_QQ_DEF)));
                    }
                }
            }
        };
    }
}

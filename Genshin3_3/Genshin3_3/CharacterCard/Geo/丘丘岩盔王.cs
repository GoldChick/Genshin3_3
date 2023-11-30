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
            new Effect_QQ_Passive()
        };
        public override ElementCategory CharacterElement => ElementCategory.Geo;
        public override WeaponCategory WeaponCategory => WeaponCategory.Other;
        public override CharacterRegion CharacterRegion => CharacterRegion.QQ;
        public override CharacterCategory CharacterCategory => CharacterCategory.Mob;
        public override string NameID => "qq";

    }
    public class Effect_QQ_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => true;
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
    public class Talent_QQ : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "qq";

        public override int Skill => 2;

        public override CostInit Cost => new CostCreate().Geo(4).MP(2).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.Die,(me,p,s,v)=>
                {
                    if (me.TeamIndex!=s.TeamID)
                    {
                        me.AddPersistent(new Effect_QQ_Passive(), p.PersistentRegion);
                        me.AddPersistent(new Effect_QQ_ATK(), p.PersistentRegion, me.Characters[p.PersistentRegion].Effects.Find(typeof(Effect_QQ_Passive)));
                    }
                }
            }
        };
    }
}

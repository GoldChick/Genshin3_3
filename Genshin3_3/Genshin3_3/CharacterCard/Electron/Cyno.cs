using TCGBase;

namespace Genshin3_3
{
    public class Cyno : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Electro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterSimpleE(4,3),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Electro(4).MP(2).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Cyno_Passive(),c.Index),new DamageVariable(4,4)),
            new Effect_Cyno_Passive()
        };

        public override ElementCategory CharacterElement => ElementCategory.Electro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Longweapon;

        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;
    }
    public class Effect_Cyno_Passive : AbstractCardSkillPassive
    {
        public override bool TriggerOnce => false;
        public override int InitialUseTimes => 0;
        public override int MaxUseTimes => 6;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundStep,(me,p,s,v)=>
            {
                p.AvailableTimes++;
                if (p.AvailableTimes>=6)
                {
                    p.AvailableTimes -= 4;
                }
            }},
            { SenderTag.ElementEnchant,(me,p,s,v)=>
            {
                if (PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv))
                {
                    if (p.AvailableTimes>=2)
                    {
                         dv.Element=4;
                        if (p.AvailableTimes>=4)
                        {
                             dv.Damage+=2;
                        }
                    }
                }
            } }
        };
        public override void Update<T>(PlayerTeam me, Persistent<T> persistent)
        {
            persistent.AvailableTimes += 2;
            if (persistent.AvailableTimes >= 6)
            {
                persistent.AvailableTimes -= 4;
            }
        }
    }
    public class Talent_Cyno : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "cyno";

        public override CostInit Cost => new CostCreate().Electro(3).ToCostInit();
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.DamageIncrease,(me,p,s,v)=>
            {
                var passive=me.Characters[p.PersistentRegion].Effects.Find(typeof(Effect_Cyno_Passive));
                if (passive!=null && passive.AvailableTimes%2==0 && PersistentFunc.IsCurrCharacterDamage(me,p,s,v,out var dv)&& dv.DirectSource==DamageSource.Character && s is PreHurtSender hs && hs.RootSource is AbstractCardSkill skill && skill.Category==SkillCategory.E)
                {
                    dv.Damage++;
                }
            } }
        };
    }
}

using TCGBase;

namespace Genshin3_3
{
    /// <summary>
    /// 奥兹换班霜华矢，唯此一心冰灵珠！
    /// </summary>
    public class Ganyu : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,1,new Effect_Ganyu(),false),
            new CharacterSimpleSkill
            (
                SkillCategory.A,
                new CostCreate().Cryo(5).ToCostInit(),
                (skill,me,c,args)=>me.AddPersistent(new Effect_Ganyu_Counter(), c.Index),
                new DamageVariable(1, 2), new DamageVariable(-1, 2, 0, true)
            ),
            new CharacterSimpleSkill(SkillCategory.Q,new CostCreate().Cryo(3).MP(3).ToCostInit(),
                (skill,me,c,args)=>me.AddSummon(new Summon_Ganyu()),
                new DamageVariable(1,2),new DamageVariable(-1,1,0,true)),
        };
        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "ganyu";
    }
    public class Effect_Ganyu : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
            {
                {SenderTag.HurtDecrease, new PersistentPurpleShield(1)}
            };

        public override string NameID => "effect_ganyu";
    }
    public class Effect_Ganyu_Counter : AbstractCardPersistent
    {
        public override int MaxUseTimes => 1;

        public override PersistentTriggerDictionary TriggerDic => new();
    }
    public class Summon_Ganyu : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.MultiHurt(new DamageVariable[]{new(1,1,0),new(-1,1,0,true) },this,()=>p.AvailableTimes--)}
        };
        public override string NameID => "summon_ganyu";
    }
    public class Talent_Ganyu : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "ganyu";
        public override int Skill => 2;
        public override CostInit Cost => new CostCreate().Cryo(5).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            me.Enemy.MultiHurt(new DamageVariable[] { new(1, 2, 0), new(-1, c.Effects.Contains(typeof(Effect_Ganyu_Counter)) ? 3 : 2, 0, true) }, c.Card.Skills[2]);
        }
    }
}

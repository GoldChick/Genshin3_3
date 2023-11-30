using TCGBase;

namespace Genshin3_6
{
    public class Tighnari : AbstractCardCharacter
    {
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleSkill(SkillCategory.A,new CostCreate().Void(2).Dendro(1).ToCostInit(),new DamageVariable(0,2)),
            new CharacterEffectE(6,2,new Effect_Tighnari()),
            new Q()
        };

        public override ElementCategory CharacterElement => ElementCategory.Dendro;

        public override WeaponCategory WeaponCategory => WeaponCategory.Bow;

        public override CharacterRegion CharacterRegion => CharacterRegion.SUMERU;
        private class Q : AbstractCardSkill
        {
            public override CostInit Cost => new CostCreate().Dendro(3).MP(2).ToCostInit();

            public override SkillCategory Category => SkillCategory.Q;

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.MultiHurt(new DamageVariable[] { new(6, 4), new(-1, 1, 0, true) }, this);
            }
        }
    }
    public class Effect_Tighnari : AbstractCardPersistent
    {
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.UseDiceFromSkill,new PersistentDiceCostModifier<UseDiceFromSkillSender>(
                (me,p,s,v)=>me.TeamIndex==s.TeamID && s.Character.Index==p.PersistentRegion && s.Skill.Category==SkillCategory.A && me.GetDices().Sum()%2==0,
                -1,(me,p,s)=>me.Characters[p.PersistentRegion].Effects.Contains(typeof(Talent_Tighnari))?1:0,false,(me,p,s)=>p.Data=114
            )},
            { SenderTag.ElementEnchant,(me,p,s,v)=>
            {
                if (p.Data!=null && v is DamageVariable dv)
                {
                    dv.Element=6;
                }
            }
            },
            { SenderTag.AfterUseSkill,(me,p,s,v)=>
            {
                if (p.Data!=null)
                {
                    me.AddSummon(new Summon_Tighnari());
                    p.AvailableTimes--;
                    p.Data=null;
                }
            }
            }
        };
    }
    public class Summon_Tighnari : AbstractCardPersistentSummon
    {
        public override int InitialUseTimes => 1;
        public override int MaxUseTimes => 2;

        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.RoundOver,(me,p,s,v)=>me.Enemy.Hurt(new(6,1),this,()=>p.AvailableTimes--)}
        };
    }

    public class Talent_Tighnari : AbstractCardEquipmentOverrideSkillTalent
    {
        public override int Skill => 1;

        public override string CharacterNameID => "tighnari";

        public override CostInit Cost => new CostCreate().Dendro(4).ToCostInit();
    }
}

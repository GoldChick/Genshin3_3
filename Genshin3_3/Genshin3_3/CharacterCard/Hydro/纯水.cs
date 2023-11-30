using TCGBase;

namespace Genshin3_3
{
    public class 纯水 : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(2,1),
            new 召唤(1),
            new 召唤(2),
            new 潮涌与激流(),
        };
        public override ElementCategory CharacterElement => ElementCategory.Hydro;
        public override WeaponCategory WeaponCategory => WeaponCategory.Other;
        public override CharacterRegion CharacterRegion => CharacterRegion.None;
        public override CharacterCategory CharacterCategory => CharacterCategory.Mob;

        public override string NameID => "hydro";
        private class 召唤 : AbstractCardSkill
        {
            public int PersistentNum { get; }
            public override CostInit Cost { get; }
            public 召唤(int num)
            {
                PersistentNum = num;
                Cost = new CostCreate().Hydro(1).ToCostInit();
                for (int i = 0; i < num; i++)
                {
                    Cost.Hydro(2);
                }
            }
            public override SkillCategory Category => SkillCategory.E;
            public static AbstractCardPersistentSummon[] PersistentPool => new AbstractCardPersistentSummon[]
            {
                new Summon_Hydro_Bird(),
                new Summon_Hydro_Squirrel(),
                new Summon_Hydro_Frog()
            };
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.AddSummon(PersistentNum, PersistentPool);
            }
        }
        private class 潮涌与激流 : AbstractCardSkill
        {
            public override SkillCategory Category => SkillCategory.Q;

            public override CostInit Cost => new CostCreate().Hydro(3).MP(3).ToCostInit();

            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs)
            {
                me.Enemy.Hurt(new(2, 4 + me.Summons.Count), this);
            }
        }
        public class Summon_Hydro_Squirrel : AbstractSimpleSummon
        {
            public Summon_Hydro_Squirrel() : base(2, 2, 2)
            {
            }
        }
    }
    public class Summon_Hydro_Bird : AbstractSimpleSummon
    {
        public Summon_Hydro_Bird() : base(2, 1, 3)
        {
        }
    }
    public class Summon_Hydro_Frog : AbstractCardPersistentSummon
    {
        public override int MaxUseTimes => 2;
        public override bool CustomDesperated => true;
        public override PersistentTriggerDictionary TriggerDic => new()
        {
            { SenderTag.HurtDecrease,new PersistentPurpleShield(1)},
            { SenderTag.RoundOver,(me,p,s,v)=>
            {
                if (p.AvailableTimes==0)
                {
                    me.Enemy.Hurt(new(2,2),this,()=>p.Active=false);
                }
            }
            }
        };
    }

    public class Talent_纯水 : AbstractCardEquipmentOverrideSkillTalent
    {
        public override string CharacterNameID => "hydro";

        public override int Skill => 3;

        public override CostInit Cost => new CostCreate().Hydro(4).MP(3).ToCostInit();
        public override void TalentTriggerAction(PlayerTeam me, Character c, int[] targetArgs)
        {
            base.TalentTriggerAction(me, c, targetArgs);
            me.Summons.Copy().ForEach(p => p.AvailableTimes++);
        }
    }

}

using TCGBase;

namespace Genshin3_3
{
    public class Chongyun : AbstractCardCharacter
    {
        public override int MaxMP => 3;
        public override AbstractCardSkill[] Skills => new AbstractCardSkill[]
        {
            new CharacterSimpleA(0,2,1),
            new CharacterEffectE(1,3,new 重华叠霜领域(),false),
            new CharacterSimpleQ(1,7)
        };
        public override ElementCategory CharacterElement => ElementCategory.Cryo;

        public override WeaponCategory WeaponCategory => WeaponCategory.Claymore;

        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

        public override string NameID => "chongyun";
        public class 重华叠霜领域 : AbstractCardPersistentEffect
        {
            public override string TextureNameID => PersistentTextures.Enchant_Cryo;
            public 重华叠霜领域(bool talent = false)
            {
                TriggerDic = new()
                {
                    { SenderTag.RoundOver,(me,p,s,v)=>p.AvailableTimes--},
                    { SenderTag.ElementEnchant,new PersistentElementEnchant(1,false,talent?1:0)}
                };
            }
            public override int MaxUseTimes => 2;

            public override PersistentTriggerDictionary TriggerDic { get; }
        }
    }
    public class Talent_Chongyun : AbstractCardEquipmentFightActionTalent
    {
        public override CardPersistentTalent Effect => new E();

        public override string CharacterNameID => "chongyun";

        public override int Skill => 1;

        public override int[] Costs => new int[] { 0, 3 };
        public override void AfterUseAction(PlayerTeam me, int[] targetArgs)
        {
            var p = me.Effects.Find(typeof(Chongyun.重华叠霜领域));
            if (p != null)
            {
                p.Active = false;
            }
            base.AfterUseAction(me, targetArgs);
        }
        private class E : CardPersistentTalent
        {
            private readonly AbstractCardSkill _skill = new CharacterEffectE(1, 3, new Chongyun.重华叠霜领域(true), false);
            public override int Skill => 1;
            public override void AfterUseAction(PlayerTeam me, Character c, int[] targetArgs) => _skill.AfterUseAction(me, c, targetArgs);
        }
    }
}

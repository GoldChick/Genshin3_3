//using TCGBase;

//namespace Genshin3_3
//{
//    public class Qiqi : AbstractCardCharacter
//    {
//        public override AbstractCardSkill[] Skills => new AbstractCardSkill[] {
//        new CharacterSimpleA(1,2),
//        new CharacterSingleSummonE(new 寒冰鬼差(),1),
//        };

//        public override ElementCategory CharacterElement => ElementCategory.Cryo;

//        public override WeaponCategory WeaponCategory => WeaponCategory.Sword;

//        public override CharacterRegion CharacterRegion => CharacterRegion.LIYUE;

//        public override string NameID => "qiqi";
//        public class 寒冰鬼差 : AbstractCardPersistentSummon
//        {
//            public override int MaxUseTimes => 3;

//            public override PersistentTriggerDictionary TriggerDic => new()
//            {
//                { SenderTag.RoundOver.ToString(),(me, p, s, v) => { p.AvailableTimes --; me.Enemy.Hurt(new DamageVariable(1, 1, 0), this); }}
//            };

//            public override string NameID => "summon_qiqi";
//        }

//    }
//}

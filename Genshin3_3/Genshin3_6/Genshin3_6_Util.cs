using TCGBase;

namespace Genshin3_6
{
    public class Genshin_3_6_Util : AbstractModUtil
    {
        public override string NameSpace => "genshin3_6";

        public override string Description => "just 3 character + 2 card for 3.6 update";

        public override string Author => "Gold_Chick";

        public override string[] GetDependencies()=>Array.Empty<string>();

        public override AbstractRegister GetRegister() => new Genshin3_6_Register();
    }
}

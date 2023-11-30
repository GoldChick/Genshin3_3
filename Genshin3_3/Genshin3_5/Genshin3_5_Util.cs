using TCGBase;

namespace Genshin3_5
{
    public class Genshin_3_5_Util : AbstractModUtil
    {
        public override string NameSpace => "genshin3_5";

        public override string Description => "just 3 character + 2 card for 3.5 update";

        public override string Author => "Gold_Chick";

        public override string[] GetDependencies()=>Array.Empty<string>();

        public override AbstractRegister GetRegister() => new Genshin3_5_Register();
    }
}

using TCGBase;

namespace Genshin3_4
{
    public class Genshin_3_4_Util : AbstractModUtil
    {
        public override string NameSpace => "genshin3_4";

        public override string Description => "just 2 character + 2 talent for 3.4 update";

        public override string Author => "Gold_Chick";

        public override string[] GetDependencies()=>Array.Empty<string>();

        public override AbstractRegister GetRegister() => new Genshin3_4_Register();
    }
}

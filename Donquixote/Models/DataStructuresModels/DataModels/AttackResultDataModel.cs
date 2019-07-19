using Donquixote.Models.DataStructuresModels.EnumModels;

namespace Donquixote.Models.DataStructuresModels.DataModels
{
    public class AttackResultDataModel
    {
        public ModeEnumModel AttackMode { get; set; } = ModeEnumModel.Bomb;
        public SpeedEnumModel AttackSpeed { get; set; } = SpeedEnumModel.Normal;

        public int PhonesLoaded { get; set; } = 0;
        public int PhonesMessaged { get; set; } = 0;
        public int PhoneSkiped { get; set; } = 0;
    }
}
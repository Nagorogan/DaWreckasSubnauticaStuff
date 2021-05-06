﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Logger = QModManager.Utility.Logger;
using System.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options;
using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Handlers;

namespace CombinedItems
{
    [Menu("Combined Items")]
    public class DWConfig : ConfigFile
    {
        private const float SPIKEY_TRAP_HEALTH_MIN = 1f;
        private const float SPIKEY_TRAP_HEALTH_MAX = 15f;
        private const float SPIKEY_TRAP_HEALTH_DEFAULT = 7f;
        private const float KNIFE_DAMAGE_MIN = 20f;
        private const float KNIFE_DAMAGE_MAX = 60f;
        private const float KNIFE_DAMAGE_DEFAULT = 20f;
        private const float KNIFE_TENTACLE_DAMAGE_MIN = 2f;
        private const float KNIFE_TENTACLE_DAMAGE_MAX = 10f;
        private const float KNIFE_TENTACLE_DAMAGE_DEFAULT = 2f;
        private const float HEATBLADE_DAMAGE_MIN = 20f;
        private const float HEATBLADE_DAMAGE_MAX = 80f;
        private const float HEATBLADE_DAMAGE_DEFAULT = 20f;
        private const float HEATBLADE_TENTACLE_DAMAGE_MIN = 2f;
        private const float HEATBLADE_TENTACLE_DAMAGE_MAX = 10f;
        private const float HEATBLADE_TENTACLE_DAMAGE_DEFAULT = 2f;

        [Slider("Spikey Trap tentacle health", SPIKEY_TRAP_HEALTH_MIN, SPIKEY_TRAP_HEALTH_MAX, DefaultValue = SPIKEY_TRAP_HEALTH_DEFAULT, Id = "SpikeyTrapHealth",
            Step = 1f,
            Tooltip = "Amount of damage that must be inflicted on a tentacle to convince a Spikey Trap to let go (UWE default: 7)")]
        public float SpikeyTrapTentacleHealth = SPIKEY_TRAP_HEALTH_DEFAULT;

        [Slider("Knife damage", KNIFE_DAMAGE_MIN, KNIFE_DAMAGE_MAX, DefaultValue = KNIFE_DAMAGE_DEFAULT, Id = "KnifeDamage",
            Step = 0.1f, Format = "{0:F1}",
            Tooltip = "Base damage dealt by Survival Knife (UWE default: 20)")]
        public float KnifeDamage = KNIFE_DAMAGE_DEFAULT;

        [Slider("Knife Tentacle damage", KNIFE_TENTACLE_DAMAGE_MIN, KNIFE_TENTACLE_DAMAGE_MAX, DefaultValue = KNIFE_TENTACLE_DAMAGE_DEFAULT, Id = "KnifeTentacleDamage",
            Step = 0.1f, Format = "{0:F1}",
            Tooltip = "Damage dealt by Survival Knife to a Spikey Trap tentacle; see above (UWE default: 2.0)")]
        public float KnifeTentacleDamage = KNIFE_TENTACLE_DAMAGE_DEFAULT;

        [Slider("Heatblade damage", HEATBLADE_DAMAGE_MIN, HEATBLADE_DAMAGE_MAX, DefaultValue = HEATBLADE_DAMAGE_DEFAULT, Id = "HeatbladeDamage",
            Step = 0.1f, Format = "{0:F1}",
            Tooltip = "Base damage dealt by Heatblade. (UWE default: 20.0)")]
        public float HeatbladeDamage = HEATBLADE_DAMAGE_DEFAULT;

        [Slider("Heatblade Tentacle damage", HEATBLADE_TENTACLE_DAMAGE_MIN, HEATBLADE_TENTACLE_DAMAGE_MAX, DefaultValue = HEATBLADE_TENTACLE_DAMAGE_DEFAULT, Id = "HeatbladeTentacleDamage",
            Step = 0.1f, Format = "{0:F1}",
            Tooltip = "Damage dealt by Heatblade to a Spikey Trap tentacle; see above (UWE default: 2.0)")]
        public float HeatbladeTentacleDamage = HEATBLADE_TENTACLE_DAMAGE_DEFAULT;

        [Toggle("Absolute values on HUD", Tooltip = "If enabled, vehicle HUDs (Exosuit, Seatruck and Hoverbike) will show absolute values on the HUD, instead of percentages. (so a Hoverbike with a full, unmodified Ion Battery would show 500 energy, instead of 100)")]
        public bool bHUDAbsoluteValues = true;
    }
}
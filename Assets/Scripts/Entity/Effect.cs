using System.Collections.Generic;

namespace Entity
{
    public enum Effect
    {
        /// <summary>
        ///     Triggers on multiple attack types, buffs the attacks further.
        /// </summary>
        Attacker,

        /// <summary>
        ///     Triggers on multiple defend types, buffs the defense further.
        /// </summary>
        Defender,

        /// <summary>
        ///     Triggers on multiple mobility types, buffs the mobility further.
        /// </summary>
        Runner,

        /// <summary>
        ///     Triggers on attack and defense types, reduces incoming attack also bei own attack values.
        /// </summary>
        StrongDefender,

        /// <summary>
        ///     Triggers on attack and mobility types, triggers additional attack.
        /// </summary>
        DoubleAttacker,

        /// <summary>
        ///     Triggers on defense and mobility types, allows counter-attack.
        /// </summary>
        CounterAttacker
    }

    public static class EffectDescription
    {
        public static readonly Dictionary<Effect, string> Description =
            new()
            {
                { Effect.Attacker, "gives units more attack power" },
                { Effect.Defender, "gives units more defense power" },
                { Effect.Runner, "gives units higher mobility" },
                { Effect.StrongDefender, "units use attacking power for defense too" },
                { Effect.DoubleAttacker, "units attack twice" },
                { Effect.CounterAttacker, "when attacked unit starts a counter attack" }
            };
    }
}
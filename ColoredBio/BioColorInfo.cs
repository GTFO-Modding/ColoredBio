using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ColoredBio
{
    internal static class BioColorInfo
    {
        public static Color Hibernate;
        public static Color Detect;
        public static Color Heartbeat;
        public static Color Aggressive;
        public static Color Scout;
        public static Color ScoutTentacle;

        static BioColorInfo()
        {
            Hibernate = ColorExt.Hex("#B3B3B3");
            Detect = ColorExt.Hex("#F2CB61");
            Detect.a = 0.8f;
            Heartbeat = ColorExt.Hex("#B3B3B3");
            Aggressive = ColorExt.Hex("#FF1A1A");
            Scout = ColorExt.Hex("#FFDC1A");
            ScoutTentacle = ColorExt.Hex("#FF1A1A");
        }

        public static void SetColorByFormat(BioState state, string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return;

            var split = format.Split('|', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 2)
                return;

            var color = ColorExt.Hex(split[0].Trim());
            if (!float.TryParse(split[1].Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var scale))
            {
                return;
            }
            color.a = scale;
            SetColorByState(state, color);
        }

        public static void SetColorByState(BioState state, Color color)
        {
            switch (state)
            {
                case BioState.Hibernate:
                    Hibernate = color;
                    break;

                case BioState.Detect:
                    Detect = color;
                    break;

                case BioState.Heartbeat:
                    Heartbeat = color;
                    break;

                case BioState.Aggressive:
                    Aggressive = color;
                    break;

                case BioState.Scout:
                    Scout = color;
                    break;

                case BioState.ScoutTentacle:
                    ScoutTentacle = color;
                    break;
            }
        }

        public static Color GetColorByState(BioState state)
        {
            switch (state)
            {
                case BioState.Hibernate:
                    return Hibernate;

                case BioState.Detect:
                    return Detect;

                case BioState.Heartbeat:
                    return Heartbeat;

                case BioState.Aggressive:
                    return Aggressive;

                case BioState.Scout:
                    return Scout;

                case BioState.ScoutTentacle:
                    return ScoutTentacle;
            }

            return Hibernate;
        }
    }

    internal enum BioState
    {
        None,
        Hibernate,
        Detect,
        Heartbeat,
        Aggressive,
        Scout,
        ScoutTentacle
    }
}

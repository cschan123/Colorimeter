using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorimeterService.Service.impl;
using ColorimeterService.Service;
using System.ComponentModel;
using System.Reflection;


namespace ColorimeterService.Utils
{
    public class ColorStandardHelper
    {
        private static AbstractColorStandard colorStandard;
        private static DescriptionAttribute attr;
        private const string _CLASSPATH = "ColorimeterService.Service.impl.";

        public static ColorStandard match(String name)
        {
            ColorStandardEnum[] values = (ColorStandardEnum[])Enum.GetValues(typeof(ColorStandardEnum));
            foreach (ColorStandardEnum value in values)
            {
                if (name.Equals(Enum.GetName(value.GetType(), value)))
                {
                    FieldInfo info = value.GetType().GetField(name);
                    attr = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute), false);
                    Type type = Type.GetType(_CLASSPATH + attr.Description);
                    colorStandard = (AbstractColorStandard)Activator.CreateInstance(type);
                    return new ColorStandard()
                    {
                        cs = colorStandard,
                        idx = (int)value
                    };
                }
            }
            return new ColorStandard() { };
        }
    }

    public struct ColorStandard
    {
        public AbstractColorStandard cs;
        public int idx;
    }

    [Description("颜色标准")]
    public enum ColorStandardEnum
    {
        [Description("ArtificialElectronicsColorStandard")]
        AECS,

        [Description("ArtificialObjectsColorStandard")]
        AOCS,

        [Description("StandardTemplateColorStandard")]
        STCS,

        [Description("SelfCalibrationColorStandard")]
        SCCS,

        [Description("LIMSColorStandard")]
        LIMS
    }
}

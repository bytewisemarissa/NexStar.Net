using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexStar.Net.Enums
{
    public class TelescopeModel
    {
        public static readonly TelescopeModel GpsSeries = new TelescopeModel(1, "GPS Series");
        // ReSharper disable once InconsistentNaming
        public static readonly TelescopeModel ISeries = new TelescopeModel(3, "i-Series");
        // ReSharper disable once InconsistentNaming
        public static readonly TelescopeModel ISeriesSe = new TelescopeModel(4, "i-Series SE");
        public static readonly TelescopeModel Cge = new TelescopeModel(5, "CGE");
        public static readonly TelescopeModel AdvancedGt = new TelescopeModel(6, "Advanced GT");
        public static readonly TelescopeModel Slt = new TelescopeModel(7, "SLT");
        public static readonly TelescopeModel Cpc = new TelescopeModel(9, "CPC");
        public static readonly TelescopeModel Gt = new TelescopeModel(10, "GT");
        public static readonly TelescopeModel FourFiveSe = new TelescopeModel(11, "4/5 SE");
        public static readonly TelescopeModel SixEightSe = new TelescopeModel(12, "6/8 SE");
        public static readonly TelescopeModel Lcm = new TelescopeModel(15, "LCM");

        public readonly Int16 Identifier;
        public readonly string ModelName;

        private TelescopeModel(Int16 identifier, string modelName)
        {
            Identifier = identifier;
            ModelName = modelName;
        }


        public static TelescopeModel GetTelescopeModelById(Int16 identifier)
        {
            switch (identifier)
            {
                case 1:
                    return GpsSeries;
                case 3:
                    return ISeries;
                case 4:
                    return ISeriesSe;
                case 5:
                    return Cge;
                case 6:
                    return AdvancedGt;
                case 7:
                    return Slt;
                case 9:
                    return Cpc;
                case 10:
                    return Gt;
                case 11:
                    return FourFiveSe;
                case 12:
                    return SixEightSe;
                case 15:
                    return Lcm;
                default:
                    return new TelescopeModel(identifier, "Unknown");
            }
        }

        public static TelescopeModel GeTelescopeModelById(byte identifier)
        {
            return GetTelescopeModelById(Convert.ToInt16(identifier));
        }
    }
}

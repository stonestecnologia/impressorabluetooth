using System.Text;

namespace ImpressoraBluetooth.Service
{
    public static class ESCPOS
    {
        public static string ESC = "\x1B";
        public static string FF = "\x0C";
        public static string GS = "\x1D";
        public static string LF = "\x0A";
        public static string RS = "\x1E";
        public static string CR = "\x0D";

        public static string PAGE_MODE = "\x1B\x4C";
        public static string SET_PRINT_AREA = "\x1B\x57";
        public static string STANDARD_MODE = "\x1D\x55";
        public static string INIT_PRINTER = "\x1B\x40";
        public static string INIT_PRINTER_ESCPOS = ESC + "@";
        public static string DC1 = "\x11";
        public static string DC2 = "\x12";
        public static string DC3 = "\x13";
        public static string RULED_LINE_START = "\x13\x28";
        public static string RULED_LINE_CLEAR = "\x13\x43";
        public static string RULED_LINE_ON = "\x13\x2B";
        public static string RULED_LINE_OFF = "\x13\x2D";
        public static string RULED_LINE_A = "\x13\x41";
        public static string RULED_LINE_PRINT = "\x13\x50";
        public static string RULED_LINE_SET = "\x13\x4C";
        public static string RULED_LINE_PATTERNSET = "\x13\x46";
        public static string LINE_PRINT = "\x13\x50";
        public static string TEXT_LEFT = "\x1B\x61\x0";
        public static string TEXT_CENTER = "\x1B\x61\x1";
        public static string TEXT_RIGHT = "\x1B\x61\x2";
        public static string SELF_TEST = "\x1B\x54";
        public static string BUZZ = "\x07";
        public static string FONT_A = "\x1B\x21\x0";
        public static string FONT_B = "\x1B\x21\x01";
        public static string CHAR_SPC_0 = "\x1B\x21\x0";
        public static string CHAR_SPC_1 = "\x1B\x21\x0";
        public static string CHAR_SPC_5 = "\x1B\x20\x05";
        public static string CHAR_SPC_10 = "\x1B\x20\x0A";
        public static string LN_SPC_0 = "\x1B\x33\x00";
        public static string LN_SPC_1 = "\x1B\x33\x01";
        public static string LN_SPC_2 = "\x1B\x33\x02";
        public static string LN_SPC_3 = "\x1B\x33\x03";
        public static string LN_SPC_4 = "\x1B\x33\x04";
        public static string LN_SPC_5 = "\x1B\x33\x05";
        public static string LN_SPC_10 = "\x1B\x33\x0A";
        public static string DOUBLE_HEIGHT = "\x1B\x21\x10";
        public static string BOLD = "\x1B\x21\x08";
        public static byte[] FEED_LINE = { 10 };


        public static string DrawLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ESCPOS.LF);
            sb.Append(ESCPOS.TEXT_CENTER).Append("-----------------------------------------------");
            sb.Append(ESCPOS.LF);
            return sb.ToString();
        }

        public static string InitPrintter(int tipoImpressora)
        {
            if (tipoImpressora != 2)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ESCPOS.INIT_PRINTER_ESCPOS);
                sb.Append(ESCPOS.BUZZ); // Bip impressora
                return sb.ToString();
            }
            return ESCPOS.LF;
        }
    }
}

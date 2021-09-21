namespace System
{
    public static class OltSystemDrawingExtensions
    {
        /// <summary>
        /// Return the Hex Value for a Color
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToHex(this System.Drawing.Color self)
        {
            return $"#{self.R:X2}{self.G:X2}{self.B:X2}";
        }

        /// <summary>
        /// Returns RBG(RedValue,GreenValue,BlueValue) 
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToRGB(this System.Drawing.Color self)
        {
            return $"RGB({self.R},{self.G},{self.B})";
        }
    }
}
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("24FaylRks4bI1RGlAt/Fm+xjeUOnVHQE4MxxNDPQGConv8fiWbwMMsFCTENzwUJJQcFCQkOJYqFudbE1pBDSW6ET1AW/cGS1v54YrfaUwWzH1+0aIu9R3euiXSGU/dyU1rx00FWAehmiNpGEnuMZ3DVeD8kxjZgGROMmX7WQrG96QRk/cSoJVfkP4Y+Vv5FIIeG7R9OaIElsouaYIE4SxpVAHKvPdqWjuy36VuMnIgySaECKpevtMxmZ+/fU7OYNO6ydcjEvvMV1sMEwOGUj7jyjwtkNYdFZTe5ldD1Bp23eOp7hvXRyzuFEl2TYRrA1nMkv2ZSDOUPyfnDK6oSs3fCgxalzwUJhc05FSmnFC8W0TkJCQkZDQBebaOgFOgmiOkFAQkNC");
        private static int[] order = new int[] { 7,6,6,4,12,6,8,11,8,13,12,12,13,13,14 };
        private static int key = 67;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}

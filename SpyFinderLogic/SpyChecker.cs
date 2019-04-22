namespace SpyFinderLogic
{
    public static class SpyChecker
    {
        public static bool MessageContainsSpy(int[] message, int[] spyCode) {
        /*
            Note: this method of comparison will return false in a case such as message = [1, 0], code = [10] : Case A
            If Case A represents a valid match, then converting message and code to strings will report a match for Case A
            However, this method would also report a match for message = [-2, 1, 0], code = [-10] : Case B
            It is ambiguous from the test cases shown whether cases A and B should represent matches or not.
            Therefore, such cases should be checked with the requirements team to ensure the correct cases are supported, possibly requiring a more complex comparison scheme
        */
            var codePosition = 0;
            foreach (var integer in message) 
            {
                if (integer != spyCode[codePosition]) continue;
                if (++codePosition == spyCode.Length) return true;
            }
            return false; 
        }
    }
}

namespace OLT.Core
{
    public static class OltResultHelper
    {
        public static IOltResult Success => new OltResultSuccess();
        public static IOltResultValidation Valid => new OltResultValid();
    }
}
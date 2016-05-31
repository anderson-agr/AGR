using System;

namespace AGR.Entidade.Exceptions
{
    public class ObrigatorioException : Exception
    {
        public ObrigatorioException(string message)
            :base(message)
        {
            
        }
    }
}
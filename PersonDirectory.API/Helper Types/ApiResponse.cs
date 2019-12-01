using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PersonDirectory.API.HelperTypes
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RequestLogger.Helpers
{
    public static class RequestHelpers
    {
        public static async Task<string> GetBodyContentAsStringAsync(HttpRequest request)
        {
            string content;
            
            //todo get request encoding and use in StreamReder
            using (Stream bodySream = request.Body)
            using (StreamReader readStream = new StreamReader(bodySream))
            {
                content = await readStream.ReadToEndAsync();
            }

            return content;
        }
    }
}

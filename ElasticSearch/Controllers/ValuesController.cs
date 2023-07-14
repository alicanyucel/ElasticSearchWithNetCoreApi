using ElasticSearch.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace ElasticSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        AppDbContext context = new();
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateData()
        {
            IList<Travel> travels = new List<Travel>();
            var random=new Random();
            for (int i=0; i < 10000;i++)
             {
                var title = new string(Enumerable.Repeat("abcçdefghıijklmnoöprsştuüvyz", 5).Select(s => s[random.Next(s.Length)]).ToArray());

                var words = new List<string>();
                for (int j=0; j < 500;j ++)
                    {
                    words.Add(new string(Enumerable.Repeat("abcçdefghıijklmnoöprsştuüvyz", 5).Select(s => s[random.Next(s.Length)]).ToArray()));
                    
                }
                var description = string.Join("", words);
                var travel = new Travel()
                {
                    Title = title,
                    Description = description
                };
                travels.Add(travel);
            }
            await context.Set<Travel>().AddRangeAsync(travels);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spe.sh.NET.Data.SQL
{
    public class SqlUrlRepository : IUrlRepository
    {
        private UrlRepositoryDataContext _context = new UrlRepositoryDataContext();

        public void SetupInitialData()
        {            
            if (!_context.ShortUrls.Any())
            {
                _context.ShortUrls.InsertOnSubmit(new ShortUrl()
                {
                    Id = "testhw",
                    Url = "http://www.google.com",
                    Added = DateTime.Now
                });
                _context.SubmitChanges();
            }
        }

        public string AddUrl(Uri uri)
        {
            var existingResult = _context.ShortUrls.FirstOrDefault(x => x.Url == uri.ToString());
            if (existingResult != null) return existingResult.Id;
            
            var newUrl = new ShortUrl()
            {
                Added = DateTime.Now,
                Id = getUniqueHash(),
                Url = uri.ToString()

            };
            _context.ShortUrls.InsertOnSubmit(newUrl);
            _context.SubmitChanges();

            return newUrl.Id;
        }

        private string getUniqueHash()
        {
            var chars = getChars();

            var result = "";

            var generate = true;
            while (generate)
            {
                result = generateToken(6, chars);

                generate = _context.ShortUrls.Any(x => x.Id == result);
            }

            return result;
        }

        private string generateToken(int length, List<int> chars)
        {
            Random rand = new Random();
            char[] charArray = new char[length];

            for (int i = 0; i < length; i++)
            {
                charArray[i] = (char)chars[rand.Next(0, chars.Count)];
            }

            return new String(charArray);
        }

        private List<int> getChars()
        {
            var chars = new List<int>();

            chars.AddRange(Enumerable.Range(48, 10));
            chars.AddRange(Enumerable.Range(65, 26));
            chars.AddRange(Enumerable.Range(97, 26));

            return chars;
        }

        public Uri ResolveUrl(string code, string ip, string referer)
        {
            var shortUrl = _context.ShortUrls.FirstOrDefault(x => x.Id == code);
            if (shortUrl != null)
            {
                var now = DateTime.Now;

                var trackerData = new TrackerData()
                {
                    IP = ip,
                    Referer = referer,
                    UrlId = shortUrl.Id,
                    Visited = now
                };

                _context.TrackerDatas.InsertOnSubmit(trackerData);

                shortUrl.LastVisited = now;

                _context.SubmitChanges();

                return new Uri(shortUrl.Url);
            }
            else return null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
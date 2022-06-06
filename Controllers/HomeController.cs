using Bank_DB_MVC_Redis_DC.Data.Bank_DB;
using Bank_DB_MVC_Redis_DC.Models;
using Bank_DB_MVC_Redis_DC.Models.Bank_DB;
using Bank_DB_MVC_Redis_DC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Bank_DB_MVC_Redis_DC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Bank_DB_Context _context;
        private readonly IDistributedCache _distributedCache;
        private Bank_Tabelle[] Query;

        public HomeController(ILogger<HomeController> logger, Bank_DB_Context context, IDistributedCache distributedCache)
        {
            _logger = logger;
            _context = context;
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SeedAsync()
        {
            Query = null;

            string recordKey = "Query_" + DateTime.Now.ToString("yyyyMMdd_hh");

            Query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (Query is null)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle select Blztabelle;

                if (Banken.Count() == 0)
                {
                    FileStream streamREADER0 = new FileStream(@"Data\Bank_DB\Bank_Tabelle-w-o-PK.json", FileMode.OpenOrCreate);
                    StreamReader streamREADER1 = new StreamReader(streamREADER0);

                    IEnumerable<Bank_Tabelle> blztabelle = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamREADER1.ReadToEnd());

                    streamREADER1.Close();
                    streamREADER0.Close();

                    using (_context)
                    {
                        foreach (Bank_Tabelle entry in blztabelle)
                        {
                            _context.Add(entry);
                        }

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                        _context.SaveChanges();

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");
                    }

                    ViewBag.blztabelle = blztabelle.ToList();
                    Query = blztabelle.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, Query);
                }

                else
                {
                    ViewBag.blztabelle = Banken.ToList();
                    Query = Banken.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, Query);
                }
            }

            else
            {
                ViewBag.blztabelle = Query;
            }

            return View();
        }

        public IActionResult Database()
        {
            using (_context)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle select Blztabelle;

                //List<BLZTabelle> list = new List<BLZTabelle>(Banken);

                ViewBag.blztabelle = Banken.ToList();

                return View();
            }
        }

        [HttpPost]
        public IActionResult NameQuery(string name)
        {
            using (_context)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle
                             where Blztabelle.Bezeichnung.ToUpper().Contains(name.ToUpper()) || Blztabelle.Kurzbezeichnung.ToUpper().Contains(name.ToUpper())
                             select Blztabelle;

                //int queryCOUNTER = 0;

                //foreach (BLZTabelle bankname in Banken)
                //{
                //    queryCOUNTER++;
                //}

                //BLZTabelle[] queryARRAY = new BLZTabelle[queryCOUNTER];
                //int arrayINDEX = 0;

                //foreach (BLZTabelle bankname in Banken)
                //{
                //    queryARRAY[arrayINDEX] = bankname;
                //    arrayINDEX++;
                //}

                ViewBag.blztabelle = Banken.ToList();

                return View();
            }
        }

        [HttpPost]
        public IActionResult PLZquery(int PLZ)
        {
            using (_context)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle
                             where Blztabelle.PLZ == PLZ
                             select Blztabelle;

                ViewBag.blztabelle = Banken.ToList();

                return View();
            }
        }

        [HttpPost]
        public IActionResult OrtQuery(string ort)
        {
            using (_context)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle
                             where Blztabelle.Ort.ToUpper().Contains(ort.ToUpper())
                             select Blztabelle;

                ViewBag.blztabelle = Banken.ToList();

                return View();
            }
        }

        [HttpPost]
        public IActionResult BLZquery(string BLZ)
        {
            using (_context)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle
                             where Blztabelle.BLZ == BLZ
                             select Blztabelle;

                ViewBag.blztabelle = Banken.ToList();

                return View();
            }
        }

        [HttpPost]
        public IActionResult BICquery(string BIC)
        {
            using (_context)
            {
                var Banken = from Blztabelle in _context.Bank_Tabelle
                             where Blztabelle.BIC.ToUpper().Contains(BIC.ToUpper())
                             select Blztabelle;

                ViewBag.blztabelle = Banken.ToList();

                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

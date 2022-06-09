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
        private Bank_Tabelle[] _query;
        //private string _recordKey;

        public HomeController(ILogger<HomeController> logger, Bank_DB_Context context, IDistributedCache distributedCache)
        {
            _logger = logger;
            _context = context;
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> IndexAsync()
        {
            _query = null;

            string recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle select modelData;

                if (queryResult.Count() == 0)
                {
                    FileStream fileStream = new FileStream(@"Data\Bank_DB\Bank_Tabelle-w-o-PK.json", FileMode.OpenOrCreate);
                    StreamReader streamReader = new StreamReader(fileStream);

                    IEnumerable<Bank_Tabelle> jsonData = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamReader.ReadToEnd());

                    streamReader.Close();
                    fileStream.Close();

                    using (_context)
                    {
                        foreach (Bank_Tabelle dataSet in jsonData)
                        {
                            _context.Add(dataSet);
                        }

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                        _context.SaveChanges();

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");
                    }

                    //ViewBag.QueryResult = jsonData.ToList();
                    _query = jsonData.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, _query);
                }

                else
                {
                    //ViewBag.QueryResult = queryResult.ToList();
                    _query = queryResult.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, _query);
                }
            }

            else
            {
                //ViewBag.QueryResult = _query;
            }

            return View();
        }

        public async Task<IActionResult> SeedAsync()
        {
            _query = null;

            string recordKey = "Seed_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "Seed_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle select modelData;

                if (queryResult.Count() == 0)
                {
                    FileStream fileStream = new FileStream(@"Data\Bank_DB\Bank_Tabelle-w-o-PK.json", FileMode.OpenOrCreate);
                    StreamReader streamReader = new StreamReader(fileStream);

                    IEnumerable<Bank_Tabelle> jsonData = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamReader.ReadToEnd());

                    streamReader.Close();
                    fileStream.Close();

                    using (_context)
                    {
                        foreach (Bank_Tabelle dataSet in jsonData)
                        {
                            _context.Add(dataSet);
                        }

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                        _context.SaveChanges();

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");
                    }

                    ViewBag.QueryResult = jsonData.ToList();
                    _query = jsonData.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, _query);
                }

                else
                {
                    ViewBag.QueryResult = queryResult.ToList();
                    _query = queryResult.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, _query);
                }
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
        }

        public async Task<IActionResult> DatabaseAsync()
        {
            _query = null;

            string recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "Database_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle select modelData;

                if (queryResult.Count() == 0)
                {
                    FileStream fileStream = new FileStream(@"Data\Bank_DB\Bank_Tabelle-w-o-PK.json", FileMode.OpenOrCreate);
                    StreamReader streamReader = new StreamReader(fileStream);

                    IEnumerable<Bank_Tabelle> jsonData = JsonSerializer.Deserialize<IEnumerable<Bank_Tabelle>>(streamReader.ReadToEnd());

                    streamReader.Close();
                    fileStream.Close();

                    using (_context)
                    {
                        foreach (Bank_Tabelle dataSet in jsonData)
                        {
                            _context.Add(dataSet);
                        }

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] ON");

                        _context.SaveChanges();

                        // doesn't work with Entity Framework Core ???
                        //_context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [Bank_Tabelle] OFF");
                    }

                    ViewBag.QueryResult = jsonData.ToList();
                    _query = jsonData.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, _query);
                }

                else
                {
                    ViewBag.QueryResult = queryResult.ToList();
                    _query = queryResult.ToArray();

                    await _distributedCache.SetRecordAsync(recordKey, _query);
                }
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NameQueryAsync(string name)
        {
            _query = null;

            string recordKey = "NameQuery_" + name + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "NameQuery_" + name + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.Bezeichnung.ToUpper().Contains(name.ToUpper()) || modelData.Kurzbezeichnung.ToUpper().Contains(name.ToUpper())
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToList();
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PLZqueryAsync(int PLZ)
        {
            _query = null;

            string recordKey = "PLZquery_" + PLZ.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "PLZquery_" + PLZ.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.PLZ == PLZ
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToList();
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrtQueryAsync(string ort)
        {
            _query = null;

            string recordKey = "OrtQuery_" + ort + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "OrtQuery_" + ort + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.Ort.ToUpper().Contains(ort.ToUpper())
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToList();
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BLZqueryAsync(string BLZ)
        {
            _query = null;

            string recordKey = "BLZquery_" + BLZ + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "BLZquery_" + BLZ + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.BLZ == BLZ
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToList();
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BICqueryAsync(string BIC)
        {
            _query = null;

            string recordKey = "BICquery_" + BIC + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            //_recordKey = "BICquery_" + BIC + "_" + DateTime.Now.ToString("yyyyMMdd_hh");

            _query = await _distributedCache.GetRecordAsync<Bank_Tabelle[]>(recordKey);

            if (_query is null)
            {
                var queryResult = from modelData in _context.Bank_Tabelle
                                  where modelData.BIC.ToUpper().Contains(BIC.ToUpper())
                                  select modelData;

                ViewBag.QueryResult = queryResult.ToList();
                _query = queryResult.ToArray();

                await _distributedCache.SetRecordAsync(recordKey, _query);
            }

            else
            {
                ViewBag.QueryResult = _query;
            }

            return View();
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

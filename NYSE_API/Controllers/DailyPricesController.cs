using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NYSE.API.Model;
using NYSE.API.Models;

namespace NYSE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyPricesController : ControllerBase
    {
        private readonly DailyPriceContext _context;
        private readonly ILogger<DailyPricesController> _logger;
        
        public DailyPricesController(DailyPriceContext context, ILogger<DailyPricesController> logger)
        {
            // set the database conn string and logger file
            _context = context;
            _logger = logger;
        }

        // GET: api/helloworld
        [HttpGet("helloworld")]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            // GET all records
            try
            {
                _logger.LogInformation("Start Hello World.");
                return "Hello World";
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception while getting Hello World.");
                return StatusCode(500, "An error occured while trying to return Hello World. Please contact the developer.");
            }
        }

        // GET: api/dailyprices
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<DailyPrice>>> GetDailyPrices()
        {
            // GET top N records
            try
            { 
                _logger.LogInformation("Return all daily prices.");
                
                // get the query string
                var queryTop = HttpContext.Request.Query["top"].ToString();
                
                if (queryTop == null)
                {
                    var list = from p in _context.DailyPrice
                               orderby p.Id descending
                               select p;

                    return await list.ToListAsync();
                }
                else
                {
                    int i = 0;
                    int.TryParse(queryTop, out i);
                    var list = (from p in _context.DailyPrice
                            orderby p.Id descending
                            select p).Take(i);

                    return await list.ToListAsync();
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,"Exception while getting all price records.");
                return StatusCode(500,"An error occured while trying to return all price records. Please contact the developer.");
            }
        }


        // GET: api/dailyprices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyPrice>> GetDailyPricesById(int id)
        {
            // GET single price record for an Id
            try { 

                _logger.LogInformation($"START: GET price record for id '{id}'.");
                var daily = await _context.DailyPrice.FindAsync(id);

                if (daily == null)
                {
                    _logger.LogInformation($"Id {id} was not found.");
                    return NotFound();
                }

                _logger.LogInformation($"END: GET price record for Id '{id}'.");
                return daily;

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,$"Exception while getting a single price record for Id '{id}'.");
                return StatusCode(500,"An error occured while trying to return a price record. Please contact the developer.");
            }
        }


        // GET: api/dailyprices/symbol/axe
        [HttpGet("symbol/{stock_symbol}")]
        public async Task<ActionResult<IEnumerable<DailyPrice>>> GetDailyPricesBySymbol(string stock_symbol)
        {
            // GET all price records for a given stock symbol
            try
            {
                _logger.LogInformation($"START: GET all price records for Symbol '{stock_symbol}'.");

                // check if a symbol was supplied
                if (String.IsNullOrEmpty(stock_symbol))
                {
                    _logger.LogInformation("Stock Symbol was not supplied.");
                    return NotFound();
                }

                var prices = from p in _context.DailyPrice
                             where p.stock_symbol == stock_symbol
                             select p;

                // check if there were results. i.e the symbol is valid
                int count = prices.Count();
                if (count == 0)
                {
                    _logger.LogInformation($"No Daily Prices for Symbol '{stock_symbol}' found.");
                    return NotFound();
                }

                _logger.LogInformation($"END: GET all price records. {count} records found for Symbol '{stock_symbol}'.");
                return await prices.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,$"Exception while getting daily prices for Symbol '{stock_symbol}'.");
                return StatusCode(500,"An error occured while trying to return all Daily Prices. Please contact the developer.");
            }

        }


        // PUT: api/dailyprices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyPrice(int id, DailyPrice daily)
        {
            // PUT (i.e update) price record
            _logger.LogInformation($"START: PUT for id '{id}'.");

            if (id != daily.Id)
            {
                return BadRequest();
            }

            _context.Entry(daily).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyPriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            _logger.LogInformation($"END: PUT for id '{id}'.");
            return NoContent();
        }

        // POST: api/dailyprices
        [HttpPost(Name = "POST")]
        public async Task<ActionResult<DailyPrice>> PostDaily(DailyPrice daily)
        {
            // POST (i.e insert) a price record
            try { 
                _logger.LogInformation($"START: POST price record.");

                _context.DailyPrice.Add(daily);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"END: POST price record.");
                return CreatedAtAction("PostDaily", new { id = daily.Id }, daily);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,"Exception while posting a new price record. Check the combination of 'Date','Stock Symbol' and 'Stock Exchange' are unique.");
                return StatusCode(500, "An error occured while trying to add a new price record. Check the combination of 'Date','Stock Symbol' and 'Stock Exchange' are unique.");
            }

        }

        // DELETE: api/dailyprices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DailyPrice>> DeleteDailyPrice(int id)
        {
            // DELETE as price record
            try { 
                _logger.LogInformation($"START: DELETE for id '{id}'.");

                var daily = await _context.DailyPrice.FindAsync(id);
                if (daily == null)
                {
                    return NotFound();
                }

                _context.DailyPrice.Remove(daily);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"END: DELETE for id '{id}'.");
                return daily;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex,$"Exception while deleting a single price record for Id '{id}'.");
                return StatusCode(500,"An error occured while trying to delete a price record. Please contact the developer.");
            }

        }

        // DELETE: api/dailyprices/symbol/tst
        [HttpDelete("symbol/{stock_symbol}")]
        public async Task<ActionResult> DeleteDailyPrices(string stock_symbol)
        {
            // DELETE as price record
            try
            {
                _logger.LogInformation($"START: DELETE for symbol '{stock_symbol}'.");

                _context.RemoveRange(_context.DailyPrice.Where(x => x.stock_symbol == stock_symbol));
                await _context.SaveChangesAsync();

                _logger.LogInformation($"END: DELETE for symbol '{stock_symbol}'.");
                return StatusCode(200,"Ok");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Exception while deleting a range of price records for symbol '{stock_symbol}'.");
                return StatusCode(500, "An error occured while trying to delete a price record. Please contact the developer.");
            }

        }

        private bool DailyPriceExists(int id)
        {
            // check if record for Id exists
            return _context.DailyPrice.Any(e => e.Id == id);
        }
    }
}

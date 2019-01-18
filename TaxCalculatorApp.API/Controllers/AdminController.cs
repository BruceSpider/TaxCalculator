using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculatorApp.API.Data;
using TaxCalculatorApp.API.DTOs;
using TaxCalculatorApp.API.models;

namespace TaxCalculatorApp.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class AdminController : ControllerBase {
        private readonly DataContext _context;
        private readonly ITaxCalculatorRepository _repo;
        public AdminController (DataContext context, ITaxCalculatorRepository repo) {
            _context = context;
            _repo = repo;
        }

        [HttpGet ("postalCodes")]
        public async Task<IActionResult> GetPostalCodes () {
            // var codes = await _context.PostalCodes.ToListAsync ();
            var innerJoinQuery = from codes in _context.PostalCodes
            join types in _context.TaxCalculationTypes on codes.TaxCalculationTypeId equals types.Id
            select new { Id = codes.Id, Code = codes.Code, TaxCalculationType = types.Type, TaxTypeId = types.Id };
            return Ok (innerJoinQuery);
        }

        [HttpGet ("taxCalculationTypes")]
        public async Task<IActionResult> GetTaxCalculationTypes () {
            var types = await _context.TaxCalculationTypes.ToListAsync ();
            return Ok (types);
        }

        [HttpPost ("addPostalCode")]
        public async Task<IActionResult> AddPostalCode ([FromBody] PostalCodeDTO postalCodeDTO) {

            if (!ModelState.IsValid) {
                return BadRequest (postalCodeDTO);
            }
            var postalCode = new PostalCode ();
            postalCode.Id = postalCodeDTO.Id;
            postalCode.Code = postalCodeDTO.Code;
            postalCode.TaxCalculationTypeId = postalCodeDTO.TaxCalculationTypeId;
            _repo.Add (postalCode);

            if (await _repo.SaveAll ()) {
                return Ok ();
            }

            return BadRequest ("Could not add postal code");
        }

         [HttpPost ("updatePostalCode")]
        public async Task<IActionResult> UpdatePostalCode ([FromBody] PostalCodeDTO postalCodeDTO) {

            if (!ModelState.IsValid) {
                return BadRequest (postalCodeDTO);
            }
            var postalCode = new PostalCode ();
            postalCode.Id = postalCodeDTO.Id;
            postalCode.Code = postalCodeDTO.Code;
            postalCode.TaxCalculationTypeId = postalCodeDTO.TaxCalculationTypeId;
            _repo.Add (postalCode);

            if (await _repo.SaveAll ()) {
                return Ok ();
            }

            return BadRequest ("Could not add postal code");
        }


        [HttpDelete ("deletePostalCode/{id}")]
        public async Task<IActionResult> DeletePostalCode (int id) {

            var postalCode = await _context.PostalCodes
                .FirstOrDefaultAsync (p => p.Id == id);

            if (postalCode ==null)  return BadRequest("An error occured.");
                
            _context.PostalCodes.Remove(postalCode);
            await _context.SaveChangesAsync ();

            return Ok ();
        }
    }
}
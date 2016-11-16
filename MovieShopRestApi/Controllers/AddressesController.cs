using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MovieShopDLL;
using MovieShopDLL.Context;
using MovieShopDLL.Entities;

namespace MovieShopRestApi.Controllers
{
    [Authorize]
    public class AddressesController : ApiController
    {
        private IRepository<Address, int> _addressRepository = new DLLFacade().GetAddressRepository();

        // GET: api/Addresses
        [Authorize(Roles = "Admin")]
        public IQueryable<Address> GetAddresses()
        {
            return new EnumerableQuery<Address>(_addressRepository.ReadAll());
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        
        public IHttpActionResult GetAddress(int id)
        {
            Address address = _addressRepository.Read(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            try
            {
                _addressRepository.Update(address);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]

        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _addressRepository.Create(address);

            return CreatedAtRoute("DefaultApi", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = _addressRepository.Read(id);
            if (address == null)
            {
                return NotFound();
            }

            _addressRepository.Delete(id);

            return Ok(address);
        }

        private bool AddressExists(int id)
        {
            return _addressRepository.ReadAll().Count(e => e.Id == id) > 0;
        }
    }
}
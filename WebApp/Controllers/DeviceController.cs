using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using WebApp.Data;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/Device")]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceDbContext deviceDb;

        public DeviceController(DeviceDbContext deviceDb) {  this.deviceDb = deviceDb; }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<DeviceCreate> CreateDevice([FromBody] DeviceCreate device)
        {
            if(device != null) {
                Device device1 = new Device();
                device1.Name = device.Name;
                device1.Code = device.Code;
                device1.IsActive = device.IsActive;

                deviceDb.Devices.Add(device1);
                deviceDb.SaveChanges();
                return Ok(device);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<DeviceDto>> GetDevices()
        {
            var devices = deviceDb.Devices;
            //return devices;
            if (devices != null)
            {
                return Ok(devices);
            }
            else { return NoContent(); }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<DeviceDto> GetByIdDevices([FromRoute] int id)
        {
         var devices = deviceDb.Devices.FirstOrDefault(g=> g.Id == id);
            if(id !=0 && devices != null)
            {
                return Ok(devices);
            }else 
            { 
                return NoContent(); 
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DeviceUpdate> UpdateDevice([FromRoute] int id , [FromBody] DeviceUpdate newDevice)
        {
            var oldDevice = deviceDb.Devices.FirstOrDefault(update => update.Id == id);
            if (id != 0)
            {
                if (oldDevice != null && newDevice != null)
                {
                    oldDevice.Name = newDevice.Name;
                    oldDevice.Code = newDevice.Code;
                    oldDevice.IsActive = newDevice.IsActive;
                    //deviceDb.Devices.Update(newDevice);
                    deviceDb.SaveChanges();
                    return Ok(oldDevice);
                }
                else
                {
                    return NoContent();
                }

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<String> deleteDevice([FromRoute] int id)
        {
            var deleteDevice = deviceDb.Devices.FirstOrDefault(d => d.Id == id);
            if (id != 0)
            {
                if(deleteDevice!= null)
                {
                    deviceDb.Devices.Remove(deleteDevice);
                    deviceDb.SaveChanges();
                    return "Deleted Successfully :" + deleteDevice.Name;
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

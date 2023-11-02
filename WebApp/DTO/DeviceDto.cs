using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }     
        public bool IsActive { get; set; }
    }
}
